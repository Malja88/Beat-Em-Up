using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private new Rigidbody2D rigidbody;
    public void StayInFrontWhileJumping()
    {
        characterMovement.isJumping = true;
        characterMovement.isAttacking = false;
    }

    public void RecoverSpriteOrderAfterJump()
    {
        characterMovement.isJumping = false;
        characterMovement.isAttacking = true;
    }

    public void LockAttack()
    {
        characterMovement.isAttacking = false;
    }

    public void UnlockAttack()
    {
        characterMovement.isAttacking = true;
    }

    public void IsMovingOff()
    {
        characterMovement.isMoving = false;
        rigidbody.bodyType = RigidbodyType2D.Static;
    }

    public void isMovingOn()
    {
        characterMovement.isMoving = true;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}
