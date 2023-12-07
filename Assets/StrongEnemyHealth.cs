using UnityEngine;

public class StrongEnemyHealth : EnemyHealth
{
    [SerializeField] StrongEnemyAi enemyAi;
    void Start()
    {
        enemyAi = GetComponent<StrongEnemyAi>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    public override void EnemyDeath()
    {
        base.EnemyDeath();
        enemyAi.timer = 0;
        enemyAi.isIdle = false;
    }
}
