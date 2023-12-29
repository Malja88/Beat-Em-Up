using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
public class EnemyAI : MonoBehaviour, IAttack, IIde
{
    [SerializeField] protected Transform player;
    [Header("Body Components")]
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected Rigidbody2D rb2d;
    [SerializeField] protected Animator animator;
    [Header("AI Variables")]
    [SerializeField] protected float attackDistance;
    [SerializeField] protected float idleMovementSpeed;
    [SerializeField] protected float interval;
    [SerializeField] protected float delay;
    [SerializeField] protected float verticalAttackDistance;

    [HideInInspector] public float timer;
    [HideInInspector] public bool isIdle;
    [HideInInspector] public bool idleAction;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool canAttack;
    [HideInInspector] public bool isFlip;

    readonly GlobalStringVariables variables = new();
    private IDisposable updateSubscription;
    private IDisposable fixedUpdateSubscription;

    void Start()
    {
        updateSubscription = Observable.EveryUpdate().Subscribe(_ =>
        {
            Flip();
            DynamicSpriteRender();
        });

        fixedUpdateSubscription = Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            Idle();
            Attack();
        });
    }

    public void Flip()
    {
        if (this == null || !isFlip) return;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * -Mathf.Sign(player.position.x - transform.position.x);
        transform.localScale = scale;
    }

    public void Attack()
    {
        if (!canAttack) return;

        float distanceToPlayerX = Mathf.Abs(transform.position.x - player.position.x);
        float distanceToPlayerY = Mathf.Abs(transform.position.y - player.position.y);

        if (distanceToPlayerX < attackDistance && distanceToPlayerY < verticalAttackDistance)
        {
            rb2d.velocity = Vector2.zero;
            isIdle = false;
            isAttacking = true;          
            AttackPlayer();
        }
        else
        {
            isAttacking = false;
            isIdle = true;
        }
    }


    public void Idle()
    {
        if (!isIdle) return;

        if (!idleAction)
        {
            int randomAction = UnityEngine.Random.Range(0, 4);
            _ = PerformActionAsync(randomAction);
        }
    }

    public virtual async Task PerformActionAsync(int action)
    {
        idleAction = true;
        switch (action)
        {
            case 0: rb2d.velocity = Vector2.up * idleMovementSpeed; animator.SetBool(variables.EnemyWalk, true); break;
            case 1: rb2d.velocity = Vector2.down * idleMovementSpeed; animator.SetBool(variables.EnemyWalk, true); break;
            case 2: rb2d.velocity = Vector2.left * idleMovementSpeed; animator.SetBool(variables.EnemyWalk, true); break;
            case 3: rb2d.velocity = Vector2.right * idleMovementSpeed; animator.SetBool(variables.EnemyWalk, true); break;
            default: break;
        }

        await Task.Delay((int)(delay * 1000));
        rb2d.velocity = Vector2.zero;
        animator.SetBool(variables.EnemyWalk, false);
        await Task.Delay((int)(delay * 1000));
        idleAction = false;
    }

    protected void DynamicSpriteRender()
    {
        if (this == null || spriteRenderer == null) return;
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }

    protected void AttackPlayer()
    {      
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            animator.SetTrigger(variables.EnemyAttack);
            timer -= interval;
        }
    }

    private void OnDestroy()
    {
        updateSubscription?.Dispose();
        fixedUpdateSubscription?.Dispose();
    }
}
