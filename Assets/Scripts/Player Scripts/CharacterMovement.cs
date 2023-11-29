using UniRx;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacherController controller;
    [SerializeField] private WeaponScriptTest weaponScript;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private KeyCode jumpButton;
    [SerializeField] private KeyCode punchButton;
    [SerializeField] private KeyCode kickButton;
    [HideInInspector] public float horizontalMove, verticalMove;
    public bool isMoving;
    public bool isJumping;
    public bool isAttacking;
    public bool isRunning;
    public bool isAttackingWithWeapon;
    readonly GlobalStringVariables variables = new();

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
        });
    }

    private void CharacterMove()
    {
        if (!isMoving)
            return;
        horizontalMove = Input.GetAxisRaw(variables.HorizontalAxis);
        verticalMove = Input.GetAxisRaw(variables.VerticalAxis);
        controller.Move(horizontalMove * Time.deltaTime, verticalMove * Time.deltaTime);
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
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            controller.Run();
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            controller.DisableRun();
        }
    }

    private void CharacterItemPunch()
    {
        if (!isAttackingWithWeapon) return;
        if (Input.GetKeyDown(punchButton) || Input.GetButtonDown(variables.Punch) && !weaponScript.isPickUp)
        {
            controller.ItemPunch();
        }
    }

    private void DynamicSpriteRender()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }
}
