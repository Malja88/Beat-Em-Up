using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private BoxCollider2D comboEndTrigger;
    [SerializeField] private BoxCollider2D punchTrigger;
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
        punchTrigger.enabled = true;
    }

    public void isMovingOn()
    {
        characterMovement.isMoving = true;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        punchTrigger.enabled = false ;
    }

    public void comboEndTriggerOn()
    {
        comboEndTrigger.enabled = true;
    }

    public void comboEndTriggerOff()
    {
        comboEndTrigger.enabled = false;
    }
}
