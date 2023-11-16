using System;
using UnityEngine;

public class CharacherController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D characterRigidBody;
    [SerializeField] private Animator animator;
    GlobalStringVariables variables = new GlobalStringVariables();
    [SerializeField] private CharacterMovement movement;

    [Header("Player Attack Settings")]
    [SerializeField] private Transform rayPunch;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float hitRayDistance;
    [SerializeField] private Vector2 overlapBoxSize;
    [SerializeField] public bool isHit;
    [SerializeField] private int comboHit;
    [SerializeField] private float comboAttackTimer;
    private float currentComboAttacTimer;
    [SerializeField] public bool timerToResetCombo;

    [Header("Player Moving Speed Settings")]
    [Range(0, 1)]
    [SerializeField] private float moveSmooth;
    [SerializeField] public float runningSpeed;
    [SerializeField] private PlayerStats playerStats;
    private float currentHorizontalSpeed;
    private Vector3 currentVelocity = Vector3.zero;
    private bool faceRight = true;
    private float lastTapTime = 0f;
    private float doubleTapTimeThreshold = 0.2f;
    private void Start()
    {
        currentHorizontalSpeed = playerStats.horizontalSpeed;
        currentComboAttacTimer = comboAttackTimer;
    }

    public void Move(float hMove, float vMove)
    {
        Vector3 targetVelocity = new Vector3(hMove * currentHorizontalSpeed, vMove * playerStats.verticalSpeed);
        characterRigidBody.velocity = Vector3.SmoothDamp(characterRigidBody.velocity, targetVelocity, ref currentVelocity, moveSmooth);         

        if (hMove > 0 && !faceRight)
        {
            Flip();
        }
        if (hMove < 0 && faceRight)
        {
            Flip();
        }
    }
    /// <summary>
    /// Flip player towards controlls
    /// </summary>
    public void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0, 180, 0);
    }

    public void Jump()
    {
        animator.Play(variables.Jump);
    }

    public void Punch()
    {
        animator.Play(variables.PunchHash);
        isHit = Physics2D.OverlapBox(rayPunch.position, overlapBoxSize, 0, layerMask);

        if (isHit)
        {
            timerToResetCombo = true;
            comboHit++;
            if (comboHit >= 3)
            {
                comboHit = 0;
                animator.Play(variables.PunchFinisherHash);
                isHit = false;
            }

        }
    }

    public void TimerToComboAttack()
    {
        if (!timerToResetCombo) { return; }
        currentComboAttacTimer -= Time.deltaTime;

        if (currentComboAttacTimer <= 0)
        {
            comboHit = 0;
            currentComboAttacTimer = comboAttackTimer;
            timerToResetCombo = false;
        }
    }

    public void Run()
    {
        if ((Time.time - lastTapTime) < doubleTapTimeThreshold)
        {
            currentHorizontalSpeed *= runningSpeed;
            animator.SetBool(variables.RunHash, true);
            movement.isRunning = true;
        }
        lastTapTime = Time.time;
    }

    public void DisableRun()
    {
        currentHorizontalSpeed = playerStats.horizontalSpeed;
        animator.SetBool(variables.RunHash, false);
        movement.isRunning = false;
    }
   
}

