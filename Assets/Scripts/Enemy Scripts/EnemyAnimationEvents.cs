using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Rigidbody2D rb;
    readonly GlobalStringVariables variables = new();
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void EnemyMovementOff()
    {
        enemyAI.canAttack = false;
        enemyAI.isIdle = false;
        enemyAI.isAttacking = false;
        enemyAI.isFlip = false;
        rb.velocity = Vector2.zero;
    }

    public void EnemyMovementOn()
    {
        enemyAI.canAttack = true;
        enemyAI.isIdle = true;
        enemyAI.isFlip = true;
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
