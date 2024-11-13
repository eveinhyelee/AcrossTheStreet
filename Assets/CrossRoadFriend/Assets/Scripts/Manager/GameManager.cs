using UnityEngine;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public int levelCount = 50;
    public Camera camera = null;
    public LevelGenerator levelGenerator = null;

    private int currentCoins = 0;
    private int currentDistance = 0;
    private bool canPlay = false;
    private AudioSource effect;
    private AudioClip clip = null;

    public event Action<int> coins;
    public event Action<int> distance;
    public event Action gameOver;

    public List<ItemObject> pools;
    public Dictionary<int, Queue<GameObject>> poolDict;

    private static GameManager s_Instance;
    public static GameManager instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(GameManager)) as GameManager;
            }

            return s_Instance;
        }
    }

    private void Awake()
    {
        poolDict = new Dictionary<int, Queue<GameObject>>();
        foreach(var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i = 0; i < 3; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDict.Add(pool.id, objectPool);
        }
    }

    private void Start()
    {
        effect = GetComponent<AudioSource>();
        clip = effect.clip;

        for (int i = 0; i < levelCount; i++)
        {
            levelGenerator.RandomGenerator();
        }
    }

    public bool CanPlay()
    {
        return canPlay;
    }

    public void StartPlay()
    {
        canPlay = true;
    }

    public void UpdateCoinCount(int value)
    {
        currentCoins += value;
        effect.PlayOneShot(clip);
        coins?.Invoke(currentCoins);
    }

    public void UpdateDistanceCount()
    {
        currentDistance += 1;
        distance?.Invoke(currentDistance);
        levelGenerator.RandomGenerator();
    }

    public GameObject SpawnFromPool(int id)
    {
        if (!poolDict.ContainsKey(id)) return null;

        if (poolDict[id].Count > 0)
        {
            GameObject obj = poolDict[id].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Debug.Log("Create Item");
            var newObj = Instantiate(pools.Find(x => x.id == id).prefab);
            newObj.SetActive(false);
            poolDict[id].Enqueue(newObj);
            return newObj;
        }
    }

    public void ReturnObject(GameObject obj, int id)
    {
        if (!poolDict.ContainsKey(id)) return;

        obj.SetActive(false);
        poolDict[id].Enqueue(obj);
    }

    public void GameOver()
    {
        camera.GetComponent<CameraShake>().Shake();
        camera.GetComponent<CameraFollow>().enabled = false;
        gameOver?.Invoke();
    }
}
