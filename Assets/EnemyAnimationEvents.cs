using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private BoxCollider2D enemyPunchCollider;
    readonly GlobalStringVariables variables = new();

    public void EnemyMovementOff()
    {
        enemyAI.canAttack = false;
        enemyAI.isIdle = false;
        enemyAI.isAttacking = false;
    }

    public void EnemyMovementOn()
    {
        enemyAI.canAttack = true;
        enemyAI.isIdle = true;
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
