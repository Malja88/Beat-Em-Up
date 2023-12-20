using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [Header("Body Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Script Dependency")]
    [SerializeField] private CharacherController controller;
    [SerializeField] private PickObjectsTest pickObjectsTest;

    private Controls controls;

    [Header("Player Behaviours")]
    public bool isMoving;
    public bool isJumping;
    public bool isAttacking;
    public bool isRunning;
    public bool isAttackingWithWeapon;
    public bool isRunningWithWeapon;
    public bool isJumpKick;

    [Header("Running Behaviour")]
    private float lastTapTime = 0f;
    [SerializeField] private float doubleTapTimeThreshold;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.Enable();
        controls.Player.Run.started += Run_performed;
        controls.Player.Run.canceled += Run_canceled;
        controls.Player.Jump.performed += Jump_performed;
        controls.Player.Punch.performed += Punch_performed;
        controls.Player.ItemPunch.performed += ItemPunch_performed;
        controls.Player.Kick.performed += Kick_performed;
        controls.Player.JumpKick.performed += JumpKick_performed;
    }

    private void Start()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            CharacterMove();
            ComboTimer();
            CharacterRun();
            DynamicSpriteRender();
            CharacterRunWithWeapon();
            CharacterMoveWithWeapon();
        });
    }

    private void Run_canceled(InputAction.CallbackContext obj)
    {
        isRunning = false;
        controller.DisableRun();
        controller.DisableRunWithWeapon();
    }

    private void Run_performed(InputAction.CallbackContext obj)
    {
        float currentTime = Time.time;
        if (currentTime - lastTapTime < doubleTapTimeThreshold)
        {
            isRunning = true;
        }
        if (currentTime - lastTapTime < doubleTapTimeThreshold && isRunningWithWeapon)
        {
            controller.RunWithWeapon();
            isRunning = false;
        }
        lastTapTime = currentTime;
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        if (isJumping) return;      
            controller.Jump();

        if (!isJumping && !pickObjectsTest.canPickUp)
        {
            controller.JumpWithWeapon();
        }
    }

    private void Punch_performed(InputAction.CallbackContext obj)
    {
        if (!isAttacking) return;
        controller.Punch();
    }

    private void ItemPunch_performed(InputAction.CallbackContext obj)
    {
        if (!isAttackingWithWeapon) return;
        if (!pickObjectsTest.canPickUp)
        {
            controller.ItemPunch();
            isAttacking = false;
        }
    }

    private void CharacterMove()
    {
        if (!isMoving) return;
        Vector2 inputVector = controls.Player.Movement.ReadValue<Vector2>();
        controller.Move(inputVector.x, inputVector.y);
    }

    private void CharacterMoveWithWeapon()
    {
        if (!isMoving) return;
        if(!pickObjectsTest.canPickUp)
        {
            Vector2 inputVector = controls.Player.Movement.ReadValue<Vector2>();
            controller.MoveWithWeapon(inputVector.x, inputVector.y);
        }
    }

    private void JumpKick_performed(InputAction.CallbackContext obj)
    {
        if (isJumpKick)
        {
            controller.JumpKick();
        }
    }

    private void Kick_performed(InputAction.CallbackContext obj)
    {
        if (!isAttacking) return;
        controller.Kick();
    }

    private void ComboTimer()
    {
        controller.TimerToComboAttack();
    }

    private void CharacterRun()
    {
        if(!isRunning) return; 
        controller.Run();
    }

    private void CharacterRunWithWeapon()
    {
        if (!isRunningWithWeapon) return;
    }

    private void DynamicSpriteRender()
    {
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }
}
