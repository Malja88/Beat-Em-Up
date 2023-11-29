using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private BoxCollider2D comboEndTrigger;
    [SerializeField] private BoxCollider2D punchTrigger;
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
    }

    public void IsMovingOn()
    {
        characterMovement.isMoving = true;
    }

    public void ComboEndTriggerOn()
    {
        comboEndTrigger.enabled = true;
    }

    public void ComboEndTriggerOff()
    {
        comboEndTrigger.enabled = false;
    }

    //public void HideWeapon()
    //{
    //    weaponSpriteRenderer.enabled = false;
    //}

    //public void ShowWeapon()
    //{
    //    weaponSpriteRenderer.enabled = true;    
    //}

    public void PunchTriggerOn()
    {
        punchTrigger.enabled = true;
    }

    public void PunchTriggerOff()
    {
        punchTrigger.enabled = false;
    }
}
