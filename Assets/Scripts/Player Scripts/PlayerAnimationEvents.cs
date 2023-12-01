using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private BoxCollider2D comboEndTrigger;
    [SerializeField] private BoxCollider2D punchTrigger;
    public void JumpingOn()
    {
        characterMovement.isJumping = true;
        characterMovement.isAttacking = false;
        characterMovement.isAttackingWithWeapon = false;
        characterMovement.isRunning = false;
    }

    public void JumpingOff()
    {
        characterMovement.isJumping = false;
        characterMovement.isAttacking = true;
        characterMovement.isAttackingWithWeapon = true;
        characterMovement.isRunning = true;    
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

    public void PlayerColliderOn()
    {
       playerCollider.enabled = true;
    }

    public void PlaeyerColliderOff()
    {
        playerCollider.enabled = false;
    }
}
