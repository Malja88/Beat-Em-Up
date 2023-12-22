using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [Header("Script Dependency")]
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private PickObjectsTest pickObjects;

    [Header("Colliders")]
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private BoxCollider2D comboEndTrigger;
    [SerializeField] private BoxCollider2D punchTrigger;
    public void JumpingOn()
    {
        characterMovement.isJumping = true;
        characterMovement.isJumpKick = true;
        characterMovement.isAttacking = false;
        characterMovement.isAttackingWithWeapon = false;
        characterMovement.isRunning = false;
        //pickObjects.canThrow = false;
    }

    public void JumpingOff()
    {
        characterMovement.isJumping = false;
        characterMovement.isJumpKick = false;
        characterMovement.isAttacking = true;
        characterMovement.isRunning = false;
        //pickObjects.canThrow = true;
    }

    public void JumpingWithWeaponOn()
    {
        //characterMovement.isJumping = true;
        characterMovement.isAttackingWithWeapon = false;
        characterMovement.isRunning = false;
        pickObjects.canThrow = false;
        characterMovement.isAttacking = false;
    }

    public void JumpingWithWeaponOff()
    {
        //characterMovement.isJumping = false;
        characterMovement.isAttackingWithWeapon = true;
        characterMovement.isRunning = false;
        pickObjects.canThrow = true;
    }

    public void LockAttack()
    {
        characterMovement.isAttacking = false;
        characterMovement.isJumping = true;
    }

    public void UnlockAttack()
    {
        characterMovement.isAttacking = true;
        characterMovement.isJumping = false;
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
