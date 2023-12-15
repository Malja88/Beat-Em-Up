using UnityEngine;

public class StrongEnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] private StrongEnemyAi strongEnemyAI;
    [SerializeField] private new Rigidbody2D rigidbody2D;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Animator animator;
    readonly GlobalStringVariables variables = new();
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void EnemyMovementOff()
    {
        strongEnemyAI.isIdle = false;
        strongEnemyAI.isAttacking = false;
        strongEnemyAI.isFlip = false;
        rigidbody2D.velocity = Vector2.zero;
        strongEnemyAI.timer = 0;
    }

    public void EnemyMovementOn()
    {
        strongEnemyAI.isIdle = true;
        strongEnemyAI.isFlip = true;
    }

    public void GetUpAnimationOn()
    {
        animator.Play(variables.EnemyGetUp);
    }

    public void DisableEnemyColldier()
    {
        boxCollider.enabled = false;
    }

    public void EnableEnemyColldier()
    {
        boxCollider.enabled = true;
    }
}
