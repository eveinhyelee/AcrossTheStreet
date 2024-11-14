using UnityEngine;
using UnityEngine.InputSystem;

public interface IHit
{
    public void GetHit();
}

public class PlayerController : MonoBehaviour, IHit
{
    public float moveDistance = 1;
    private Vector3 curPos;
    private Vector3 moveValue;
    public float moveTime = 0.4f;
    public float colliderDistCheck = 1.1f;

    public ParticleSystem particle = null;
    public Transform chick = null;
    public bool isDead = false;

    void Start()
    {
        moveValue = Vector3.zero;
        curPos = transform.position;
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector3>();

        if (input.magnitude > 1f) return;

        if (context.performed)
        {
            AudioSource audioSource = GameManager.instance.audioSource;
            AudioClip jumpClip = GameManager.instance.jumpClip;
            audioSource.PlayOneShot(jumpClip);
            
            if(input.magnitude == 0f)
            {
                Moving(transform.position + moveValue);
                Rotate(moveValue);
                moveValue = Vector3.zero;
            }
            else
            {
                moveValue = input * moveDistance;
            }
        }
    }

    void Moving(Vector3 pos)
    {
        LeanTween.move(this.gameObject, pos, moveTime).setOnComplete(() => { if (pos.z > curPos.z) SetMoveForwardState(); });
    }

    void Rotate(Vector3 pos)
    {
        chick.rotation = Quaternion.Euler(270, pos.x * 90, 0);
    }

    void SetMoveForwardState()
    {
        GameManager.instance.UpdateDistanceCount();
        curPos = transform.position;
    }

    public void GetHit()
    {
        GameManager.instance.GameOver();
        isDead = true;
        ParticleSystem.EmissionModule em = particle.emission;
        em.enabled = true;
    }
}
