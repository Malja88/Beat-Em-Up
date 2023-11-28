using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private BoxCollider2D comboEndTrigger;
    [SerializeField] private BoxCollider2D punchTrigger;
    [SerializeField] private SpriteRenderer weaponSpriteRenderer;
    public void StayInFrontWhileJumping()
    {
        characterMovement.isJumping = true;
        characterMovement.isAttacking = false;
        characterMovement.isAttackingWithWeapon = false;
    }

    public void RecoverSpriteOrderAfterJump()
    {
        characterMovement.isJumping = false;
        characterMovement.isAttacking = true;
        characterMovement.isAttackingWithWeapon = true;
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

    public void IsMovingOn()
    {
        characterMovement.isMoving = true;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    public void ComboEndTriggerOn()
    {
        comboEndTrigger.enabled = true;
    }

    public void ComboEndTriggerOff()
    {
        comboEndTrigger.enabled = false;
    }

    public void HideWeapon()
    {
        weaponSpriteRenderer.enabled = false;
    }

    public void ShowWeapon()
    {
        weaponSpriteRenderer.enabled = true;    
    }

    public void PunchTriggerOn()
    {
        punchTrigger.enabled = true;
    }

    public void PunchTriggerOff()
    {
        punchTrigger.enabled = false;
    }
}
