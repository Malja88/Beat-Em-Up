using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private GameObject minimalDamagePunch;
    public void StayInFrontWhileJumping()
    {
        characterMovement.isJumping = true;
    }

    public void RecoverSpriteOrderAfterJump()
    {
        characterMovement.isJumping = false;
    }

    public void minimalDamagePunchOn()
    {
        minimalDamagePunch.SetActive(true);
    }
    public void minimalDamagePunchOff()
    {
        minimalDamagePunch.SetActive(false);
    }
}
