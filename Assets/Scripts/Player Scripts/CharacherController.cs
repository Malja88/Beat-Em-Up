using System;
using UnityEngine;

public class CharacherController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    readonly GlobalStringVariables variables = new();
    private Controls controls;

    [Header("Player Attack Settings")]
    [SerializeField] private Vector2 overlapBoxSize;
    [SerializeField] private Transform rayPunch;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private int timeToPerformFinalBlow;
    [SerializeField] private int comboHit;
    [SerializeField] private float comboAttackTimer;
    [SerializeField] private float currentComboAttackTimer;
    [SerializeField] private float hitRayDistance;
    [SerializeField] private bool timerToResetCombo;
    private bool isHit;
 

    [Header("Player Moving Speed Settings")]
    [Range(0, 1)]
    [SerializeField] private float moveSmooth;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float runningWithWeaponSpeed;
    [SerializeField] private PlayerStats playerStats;  
    private Vector3 currentVelocity = Vector3.zero;
    private bool faceRight = true;
    private float currentHorizontalSpeed;

    private void Start()
    {
        currentHorizontalSpeed = playerStats.horizontalSpeed;
        currentComboAttackTimer = comboAttackTimer;
        controls = new Controls();
        controls.Player.Enable();
    }

    public void Move(float hMove, float vMove)
    {
        //Vector3 targetVelocity = new(hMove * currentHorizontalSpeed, vMove * playerStats.verticalSpeed);
        //characterRigidBody.velocity = Vector3.SmoothDamp(characterRigidBody.velocity, targetVelocity, ref currentVelocity, moveSmooth);

        Vector2 inputVector = controls.Player.Movement.ReadValue<Vector2>();
        animator.SetBool(variables.Walk, Mathf.Abs(inputVector.x) >= 0.1f || Mathf.Abs(inputVector.y) >= 0.1f);
        Vector3 moveDirection = new Vector3(inputVector.x, inputVector.y, 0f).normalized;
        Vector3 targetPosition = transform.position + moveDirection * currentHorizontalSpeed;
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSmooth * Time.deltaTime);

        if (inputVector.x > 0 && !faceRight || inputVector.x < 0 && faceRight)
        {
            Flip();
        }
    }

    public void MoveWithWeapon(float hMove, float vMove)
    {
        Vector2 inputVector = controls.Player.Movement.ReadValue<Vector2>();
        animator.SetBool(variables.WalkingWithWeapon, Mathf.Abs(inputVector.x) >= 0.1f || Mathf.Abs(inputVector.y) >= 0.1f);
    }

    public void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0, 180, 0);
    }

    public void Jump()
    {
        animator.Play(variables.JumpHash);
    }

    public void JumpWithWeapon()
    {
        animator.Play(variables.JumpWithWeapon);
    }

    public void JumpKick()
    {
       animator.Play(variables.JumpKick);
    }

    public void Punch()
    {
        animator.Play(variables.PunchHash);
        isHit = Physics2D.OverlapBox(rayPunch.position, overlapBoxSize, 0, layerMask);
        if (isHit)
        {
            timerToResetCombo = true;
            comboHit++;
            if (comboHit >= timeToPerformFinalBlow)
            {
                comboHit = 0;
                animator.Play(variables.PunchFinisherHash);
                isHit = false;
            }
        }
    }

    public void Kick()
    {
        animator.Play(variables.KickHash);
        isHit = Physics2D.OverlapBox(rayPunch.position, overlapBoxSize, 0, layerMask);

        if (isHit)
        {
            timerToResetCombo = true;
            comboHit++;
            if (comboHit >= timeToPerformFinalBlow)
            {
                comboHit = 0;
                animator.Play(variables.KickFinisherHash);
                isHit = false;
            }

        }
    }

    public void TimerToComboAttack()
    {
        if (!timerToResetCombo) { return; }
        currentComboAttackTimer -= Time.deltaTime;

        if (currentComboAttackTimer <= 0)
        {
            comboHit = 0;
            currentComboAttackTimer = comboAttackTimer;
            timerToResetCombo = false;
        }
    }

    public void Run()
    {
        animator.SetBool(variables.RunHash, true);
        currentHorizontalSpeed = runningSpeed;       
    }

    public void DisableRun()
    {
        animator.SetBool(variables.RunHash, false);
        currentHorizontalSpeed = playerStats.horizontalSpeed;
    }
   
    public void ItemPunch()
    {
        animator.Play(variables.ItemHitHash);
    }

    public void RunWithWeapon()
    {
        animator.SetBool(variables.RunWithWeapon, true);
        currentHorizontalSpeed = runningSpeed;       
    }

    public void DisableRunWithWeapon()
    {
        animator.SetBool(variables.RunWithWeapon, false);
        currentHorizontalSpeed = playerStats.horizontalSpeed;      
    }
}

