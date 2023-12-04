using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
public class EnemyAI : MonoBehaviour, IAttack, IIde
{
    [SerializeField] protected Transform player;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected CharacterMovement characterMovement;
    [SerializeField] protected Rigidbody2D rb2d;
    [SerializeField] protected Animator animator;

    [SerializeField] protected float attackDistance;
    [SerializeField] protected float idleMovementSpeed;
    [SerializeField] protected float interval;
    [HideInInspector] protected float timer;

    public bool isIdle;
    public bool idleAction;
    public bool isAttacking;
    public bool canAttack;

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
        if (this == null)
        {
            return;
        }
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * -Mathf.Sign(player.position.x - transform.position.x);
        transform.localScale = scale;
    }

    public void Attack()
    {
        if (!canAttack) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < attackDistance)
        {
            isIdle = false;
            isAttacking = true;
            rb2d.velocity = Vector2.zero;
            animator.SetBool(variables.EnemyWalk, false);
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
        if (!isIdle)
        {
            return;
        }

        if (!idleAction)
        {
            int randomAction = UnityEngine.Random.Range(0, 4);
            float delay = 3;
            _ = PerformActionAsync(randomAction, delay);
        }
    }

    public virtual async Task PerformActionAsync(int action, float delay)
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
        if (this == null || spriteRenderer == null)
        {
            return;
        }
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
