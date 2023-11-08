using System;
using UnityEngine;

public class CharacherController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D characterRigidBody;
    [SerializeField] private Animator animator;

    [Header("Player Moving Speed Settings")]
    [Range(0, 1)]
    [SerializeField] private float moveSmooth;
    [SerializeField] public bool canMove = true;
    [SerializeField] public float horizontalSpeed;
    [SerializeField] public float verticalSpeed;
    private Vector3 currentVelocity = Vector3.zero;
    private bool faceRight = true;

    public void Move(float hMove, float vMove)
    {
        if (canMove)
        {
            Vector3 targetVelocity = new Vector3(hMove * horizontalSpeed, vMove * verticalSpeed);
            characterRigidBody.velocity = Vector3.SmoothDamp(characterRigidBody.velocity, targetVelocity, ref currentVelocity, moveSmooth);         
        }

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
        animator.Play("Jump");
    }

    public void Punch()
    {
        animator.Play("Punch");
    }
}

