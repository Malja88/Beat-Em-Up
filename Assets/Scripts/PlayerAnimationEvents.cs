using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CharacterMovement characterMovement;
    public void StayInFrontWhileJumping()
    {
       // spriteRenderer.sortingOrder = 2;
        characterMovement.isJumping = true;
    }

    public void RecoverSpriteOrderAfterJump()
    {
        //spriteRenderer.sortingOrder = 0;
        characterMovement.isJumping = false;
    }
}
