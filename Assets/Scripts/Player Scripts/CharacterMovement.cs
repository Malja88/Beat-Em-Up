using UniRx;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacherController controller;
    [SerializeField] private KeyCode jumpButton;
    [SerializeField] private KeyCode punchButton;
    [HideInInspector] public float horizontalMove, verticalMove;
    [SerializeField] public bool isMoving;
    [SerializeField] public bool isJumping;
    [SerializeField] public bool isAttacking;
    [SerializeField] public bool isRunning;
    GlobalStringVariables variables = new GlobalStringVariables();

    private void Start()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            CharacterMove();
            CharacterJump();
            CharacterPunch();
            ComboTimer();
            CharacterRun();
        });

        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);
        });
    }

    private void CharacterMove()
    {
        if (!isMoving)
            return;

            horizontalMove = Input.GetAxisRaw(variables.HorizontalAxis);
            verticalMove = Input.GetAxisRaw(variables.VerticalAxis);
            //animator.SetBool("Walk", Mathf.Abs(horizontalMove) >= 1 || Mathf.Abs(verticalMove) >= 1);
    }

    private void CharacterJump()
    {
        if (isJumping || !Input.GetKeyDown(jumpButton) && !Input.GetButtonDown(variables.Jump)) return;
        controller.Jump();
    }

    private void CharacterPunch()
    {
        if (!isAttacking) return;
        if (Input.GetKeyDown(punchButton) || Input.GetButtonDown(variables.Punch))
        {
            controller.Punch();
        }
        else if (Input.GetKeyUp(punchButton) || Input.GetButtonUp(variables.Punch))
        {
            controller.isHit = false;
        }
    }

    private void ComboTimer()
    {
        controller.TimerToComboAttack();
    }

    private void CharacterRun()
    {
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            controller.Run();
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            controller.DisableRun();
        }
    }
}
