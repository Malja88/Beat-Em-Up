using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CharacterMovement characterMovement;
    public void StayInFrontWhileJumping()
    {
        characterMovement.isJumping = true;
    }

    public void RecoverSpriteOrderAfterJump()
    {
        characterMovement.isJumping = false;
    }
}
