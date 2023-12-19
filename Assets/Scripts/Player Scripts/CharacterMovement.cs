using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CharacterMovement : MonoBehaviour
{
    [Header("Body Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    [Header("Script Dependency")]
    [SerializeField] private CharacherController controller;
    [SerializeField] private PickObjectsTest pickObjectsTest;

    [Header("Input Buttons")]
    [SerializeField] private KeyCode jumpButton;
    [SerializeField] private KeyCode punchButton;
    [SerializeField] private KeyCode kickButton;
    [SerializeField] private KeyCode jumpKickButton;
    private Controls controls;

    [Header("Player Behaviours")]
    public bool isMoving;
    public bool isJumping;
    public bool isAttacking;
    public bool isRunning;
    public bool isAttackingWithWeapon;
    public bool isRunningWithWeapon;
    readonly GlobalStringVariables variables = new();
    [HideInInspector] public float horizontalMove, verticalMove;

    private float lastTapTime = 0f;
    [SerializeField] private float doubleTapTimeThreshold;

    private void Awake()
    {
        controls = new Controls();
        controls.Player.Enable();
        controls.Player.Run.started += Run_performed;
        controls.Player.Run.canceled += Run_canceled;
    }



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
        if(!isRunning) return; 
        //if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    controller.Run();
        //}
        //if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        //{
        //    controller.DisableRun();
        //}
        controller.Run();
    }

    private void CharacterRunWithWeapon()
    {
        if (!isRunningWithWeapon) return;
        //if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    controller.RunWithWeapon();
        //}
        //if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        //{
        //    controller.DisableRunWithWeapon();
        //}
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
