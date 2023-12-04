using System;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
public class EnemyAI : MonoBehaviour, IAttack, IIde
{
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;

    [SerializeField] private float attackDistance;
    [SerializeField] private float idleMovementSpeed;
    [SerializeField] private float stunnedTime;
    [SerializeField] private float interval;
    [HideInInspector] private float timer;

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
            //SpriteBalance();
            Flip();
            DynamicSpriteRender();
        });

        fixedUpdateSubscription = Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            Idle();
            Attack();
        });
    }

    //private void SpriteBalance()
    //{
    //    if (characterMovement.isJumping && player.position.y < transform.position.y)
    //    {
    //        spriteRenderer.sortingOrder = -10;
    //    }
    //    else if (characterMovement.isJumping == false)
    //    {
    //        spriteRenderer.sortingOrder = 0;
    //    }
    //}

    private void Flip()
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
        //float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // if (distanceToPlayer < attackDistance)
        // {
        //        isIdle = false;
        //    canAttack = true;
        //        //isAttacking = true;
        //        rb2d.velocity = Vector2.zero;
        //        //Attack logic...
        // }

        //  else
        //  {
        //     canAttack = false;
        //  }
    }


    public void Idle()
    {
        if (!isIdle)
        {
           // rb2d.velocity = Vector2.zero;
            return;
        }

        if (!idleAction)
        {
            int randomAction = UnityEngine.Random.Range(0, 4);
            float delay = 3;
            _ = PerformActionAsync(randomAction, delay);
        }
    }

    public async Task PerformActionAsync(int action, float delay)
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

    private void DynamicSpriteRender()
    {
        if (this == null || spriteRenderer == null)
        {
            return;
        }
        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f) * -1;
    }

    private void AttackPlayer()
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
