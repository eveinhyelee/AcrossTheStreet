using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public bool goLeft = false;
    public bool goRight = false;

    public List<GameObject> items;
    public List<Spawner> spawnersLeft = new List<Spawner>();
    public List<Spawner> spawnersRight = new List<Spawner>();

    void Start()
    {
        int itemId1 = Random.Range(0, items.Count);
        int itemId2 = Random.Range(0, items.Count);

        ItemObject item1 = items[itemId1].GetComponent<ItemObject>();
        ItemObject item2 = items[itemId2].GetComponent<ItemObject>();

        int direction = Random.Range(0, 2);

        if (direction > 0) { goLeft = false; goRight = true; } else { goLeft = true; goRight = false; }

        for(int i = 0; i< spawnersLeft.Count; i++)
        {
            if (i % 2 != 0)
            {
                spawnersLeft[i].Item = item1;
            }
            else
            {
                spawnersLeft[i].Item = item2;
            }
            spawnersLeft[i].goLeft = goLeft;
            spawnersLeft[i].gameObject.SetActive(goRight);
            spawnersLeft[i].spawnLeftPos = spawnersLeft[i].transform.position.x;
        }


        for (int i = 0; i < spawnersRight.Count; i++)
        {
            if (i % 2 != 0)
            {
                spawnersRight[i].Item = item1;

            }
            else
            {
                spawnersRight[i].Item = item2;

            }
            spawnersRight[i].goLeft = goLeft;
            spawnersRight[i].gameObject.SetActive(goLeft);
            spawnersRight[i].spawnLeftPos = spawnersRight[i].transform.position.x;
        }
    }
}
