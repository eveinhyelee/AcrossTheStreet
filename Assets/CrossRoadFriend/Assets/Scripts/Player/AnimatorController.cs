using UnityEngine;
using UnityEngine.InputSystem;

public class AnimatorController : MonoBehaviour
{
    public PlayerController playerController = null;
    private Animator animator = null;

    private string trJump = "jump";
    private string trDead = "dead";

    private int Jump;
    private int Dead;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        Jump = Animator.StringToHash(trJump);
        Dead = Animator.StringToHash(trDead);
    }

    void Update()
    {
        if (playerController.isDead)
        {
            animator.SetBool(Dead, true);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector3 input = context.ReadValue<Vector3>();

        if (input.magnitude > 1f) return;

        if (context.performed)
        {
            if (input.magnitude == 0f)
            {
                animator.SetBool(Jump, false);
            }
            else
            {
                animator.SetBool(Jump, true);
            }
        }
    }
}
