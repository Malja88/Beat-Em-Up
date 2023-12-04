using System;
using UnityEngine;

public class CharacherController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D characterRigidBody;
    [SerializeField] private Animator animator;
    readonly GlobalStringVariables variables = new();
    [SerializeField] private CharacterMovement movement;

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
    [SerializeField] public bool isHit;
 

    [Header("Player Moving Speed Settings")]
    [Range(0, 1)]
    [SerializeField] private float moveSmooth;
    [SerializeField] private float runningSpeed;
    [SerializeField] private PlayerStats playerStats;  
    [SerializeField] private float doubleTapTimeThreshold;
    [SerializeField] private float doubleTapTimeRunThreshold;
    private Vector3 currentVelocity = Vector3.zero;
    private bool faceRight = true;
    private float lastTapTime = 0f;
    private float lastTapRunTime = 0f;
    private float currentHorizontalSpeed;

    private void Start()
    {
        currentHorizontalSpeed = playerStats.horizontalSpeed;
        currentComboAttackTimer = comboAttackTimer;

    }

    public void Move(float hMove, float vMove)
    {
        //Vector3 targetVelocity = new(hMove * currentHorizontalSpeed, vMove * playerStats.verticalSpeed);
        //characterRigidBody.velocity = Vector3.SmoothDamp(characterRigidBody.velocity, targetVelocity, ref currentVelocity, moveSmooth);
        
        animator.SetBool(variables.Walk, Mathf.Abs(hMove) >= 1 || Mathf.Abs(vMove) >= 1);
        Vector3 moveDirection = new Vector3(hMove, vMove, 0f).normalized;
        Vector3 targetPosition = transform.position + moveDirection * currentHorizontalSpeed;
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSmooth * Time.deltaTime);
        if (hMove > 0 && !faceRight || hMove < 0 && faceRight)
        {
            Flip();
        }
    }

    public void MoveWithWeapon(float hMove, float vMove)
    {
        animator.SetBool(variables.WalkingWithWeapon, Mathf.Abs(hMove) >= 1 || Mathf.Abs(vMove) >= 1);
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
        if ((Time.time - lastTapTime) < doubleTapTimeThreshold)
        {
            currentHorizontalSpeed *= runningSpeed;
            animator.SetBool(variables.RunHash, true);
        }
        lastTapTime = Time.time;
    }

    public void DisableRun()
    {
        currentHorizontalSpeed = playerStats.horizontalSpeed;
        animator.SetBool(variables.RunHash, false);
    }
   
    public void ItemPunch()
    {
        animator.Play(variables.ItemHitHash);
        movement.isAttacking = false;
    }

    public void RunWithWeapon()
    {
        if ((Time.time - lastTapRunTime) < doubleTapTimeRunThreshold)
        {
            currentHorizontalSpeed *= runningSpeed;
            animator.SetBool(variables.RunWithWeapon, true);
        }
        lastTapRunTime = Time.time;
    }

    public void DisableRunWithWeapon()
    {
        currentHorizontalSpeed = playerStats.horizontalSpeed;
        animator.SetBool(variables.RunWithWeapon, false);
    }
}

