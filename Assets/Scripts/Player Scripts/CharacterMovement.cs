using UniRx;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Body Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Script Dependency")]
    [SerializeField] private CharacherController controller;
    [SerializeField] private PickObjectsTest pickObjectsTest;

    [Header("Input Buttons")]
    [SerializeField] private KeyCode jumpButton;
    [SerializeField] private KeyCode punchButton;
    [SerializeField] private KeyCode kickButton;
    [SerializeField] private KeyCode jumpKickButton;

    [Header("Player Behaviours")]
    public bool isMoving;
    public bool isJumping;
    public bool isAttacking;
    public bool isRunning;
    public bool isAttackingWithWeapon;
    public bool isRunningWithWeapon;
    readonly GlobalStringVariables variables = new();
    [HideInInspector] public float horizontalMove, verticalMove;

    private void Start()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            CharacterMove();
            CharacterJump();
            CharacterPunch();
            ComboTimer();
            CharacterRun();
            CharacterItemPunch();
            CharacterKick();
            DynamicSpriteRender();
            CharacterRunWithWeapon();
            CharacterMoveWithWeapon();
            CharacterJumpKick();
            CharacterJumpWithWeapon();
        });
    }

    private void CharacterMove()
    {
        if (!isMoving) return;
        horizontalMove = Input.GetAxisRaw(variables.HorizontalAxis);
        verticalMove = Input.GetAxisRaw(variables.VerticalAxis);
        controller.Move(horizontalMove, verticalMove);
    }

    private void CharacterMoveWithWeapon()
    {
        if (!isMoving) return;
        if(!pickObjectsTest.canPickUp)
        {
            horizontalMove = Input.GetAxisRaw(variables.HorizontalAxis);
            verticalMove = Input.GetAxisRaw(variables.VerticalAxis);
            controller.MoveWithWeapon(horizontalMove, verticalMove);
        }
    }

    private void CharacterJump()
    {
        if (isJumping || !Input.GetKeyDown(jumpButton) && !Input.GetButtonDown(variables.Jump)) return;
        controller.Jump();
    }

    private void CharacterJumpWithWeapon()
    {
        if (!isJumping && !pickObjectsTest.canPickUp && Input.GetButtonDown(variables.Jump))
        {
            controller.JumpWithWeapon();
        }
    }

    private void CharacterJumpKick()
    {
        if(isJumping && Input.GetKeyDown(jumpKickButton))
        {
            controller.JumpKick();
            isJumping = true;
        }       
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

    private void CharacterKick()
    {
        if (!isAttacking) return;
        if (Input.GetKeyDown(kickButton) || Input.GetButtonDown(variables.Kick))
        {
            controller.Kick();
        }
        else if (Input.GetKeyUp(kickButton) || Input.GetButtonUp(variables.Kick))
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
        if(!isRunning) { return; }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            controller.Run();
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            controller.DisableRun();
        }
    }

    private void CharacterRunWithWeapon()
    {
        if (!isRunningWithWeapon) return;
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            controller.RunWithWeapon();
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            controller.DisableRunWithWeapon();
        }
    }

    private void CharacterItemPunch()
    {
        if (!isAttackingWithWeapon) return;
        if (Input.GetKeyDown(punchButton) || Input.GetButtonDown(variables.Punch) && !pickObjectsTest.canPickUp)
        {
            controller.ItemPunch();
            isAttacking = false;
        }
    }

    private void DynamicSpriteRender()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }
}
