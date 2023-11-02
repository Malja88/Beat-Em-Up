using System;
using UniRx;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private CharacherController controller;
    [SerializeField] private KeyCode jumpButton;
    [HideInInspector] public float horizontalMove, verticalMove;
    [SerializeField] private bool isMoving = true;
    [SerializeField] public bool isJumping;
    private void Start()
    {
        isJumping = false;
        Observable.EveryUpdate().Subscribe(_ =>
        {
            CharacterMove();
            CharacterJump();
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

        if (isMoving)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");
            verticalMove = Input.GetAxisRaw("Vertical");
            //if (Mathf.Abs(horizontalMove) >= 1 || Mathf.Abs(verticalMove) >= 1)
            //{
            //    animator.SetBool("Walk", true);
            //}
            //else
            //{
            //    animator.SetBool("Walk", false);
            //}
        }
    }

    private void CharacterJump()
    {
        if(Input.GetKey(jumpButton))
        {
            controller.Jump();
        }
    }


}
