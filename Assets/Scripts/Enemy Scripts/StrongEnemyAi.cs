using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class StrongEnemyAi : EnemyAI, IChase
{
    [HideInInspector] public bool canChase;
    [HideInInspector] public bool isChasing;
    [SerializeField] private float followDistance;
    [SerializeField] private float chasingSpeed;
    [Tooltip("Performs an attack by given time")]
    [SerializeField] private float attackTime;
    readonly GlobalStringVariables variables = new();
    void Start()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            Flip();
            DynamicSpriteRender();
            HeavyAttackTimer();
        });

        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            Idle();
            Chase();
            HeavyAttack();
        });
    }

    protected new virtual void Idle()
    {
        base.Idle();
    }

    protected new virtual void Flip()
    {
        base.Flip();
    }

    protected virtual void DynamicSpriteRenderer()
    {
        DynamicSpriteRender();
    }

    public void Chase()
    {
        if (this == null || !canChase) return;

        isIdle = false;
        Vector2 direction = (player.position - transform.position).normalized;
        rb2d.velocity = direction * chasingSpeed;      
        animator.SetBool(variables.EnemyRun, true);

        float distanceToPlayerX = Mathf.Abs(transform.position.x - player.position.x);
        float distanceToPlayerY = Mathf.Abs(transform.position.y - player.position.y);

        if (distanceToPlayerX < attackDistance && distanceToPlayerY < verticalAttackDistance)
        {
            isAttacking = true;
            isIdle = false;
            canChase = false;
            animator.SetBool(variables.EnemyRun, false);
            rb2d.velocity = Vector2.zero;
        }
    }

    private async void HeavyAttack()
    {
        if (!isAttacking) return;
        animator.Play(variables.EnemyHeavyAttack);
        isAttacking = false;
        await Task.Delay(1000);
        isIdle = true;
    }

    private void HeavyAttackTimer()
    {
        timer += Time.deltaTime;
        if(timer >= attackTime)
        {
            canChase = true;
            timer -= attackTime;
        }
    }
}
