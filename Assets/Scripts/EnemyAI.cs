using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IEnemy
{
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private Rigidbody2D rb2d;

    [SerializeField] private float chasingSpeed;
    [SerializeField] private float followDistance;
    [SerializeField] private float attackDistance;
    [SerializeField] private float idleMovementSpeed;

    [SerializeField] public bool isFollowing;
    [SerializeField] public bool isIdle;
    [SerializeField] public bool idleAction;
    [SerializeField] public bool isAttacking;

    void Start()
    {
        isIdle = true;
        Observable.EveryUpdate().Subscribe(_ =>
        {
            SpriteBalance();
            Flip();
        });

        Observable.EveryFixedUpdate().Subscribe(_ =>
        {
            Idle();
            AttackPlayer();
            FollowPlayer();
        });
    }
    public void AttackPlayer()
    {
        if (!isAttacking) { return; }
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < attackDistance)
        {
            rb2d.velocity = Vector2.zero;
            //Attack logic...
            isFollowing = false;
        }

        if (distanceToPlayer > attackDistance && distanceToPlayer < followDistance)
        {
            isFollowing = true;
        }
    }

    public void FollowPlayer()
    {
        if (!isFollowing) { return; }
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < followDistance)
        {
            isAttacking = false;
            Vector2 direction = (player.position - transform.position).normalized;
            rb2d.velocity = direction * chasingSpeed;
        }

        if (distanceToPlayer < attackDistance)
        {
            isAttacking = true;
            isFollowing = false;
        }

        else
        {
            isAttacking = false;
            isFollowing = false;
        }
    }

    public void Idle()
    {
        if (!isIdle && !isFollowing)
        {
            rb2d.velocity = Vector2.zero;
            return;

        }

        if (!idleAction)
        {
            int randomAction = Random.Range(0, 4);
            float delay = 3;
            _ = PerformAction(randomAction, delay);
        }

        else
        {
            isFollowing = true;
        }
    }

    private async Task PerformAction(int action, float delay)
    {
        idleAction = true;

        switch (action)
        {
            case 0: rb2d.velocity = Vector2.up * idleMovementSpeed; break;
            case 1: rb2d.velocity = Vector2.down * idleMovementSpeed; break;
            case 2: rb2d.velocity = Vector2.left * idleMovementSpeed; break;
            case 3: rb2d.velocity = Vector2.right * idleMovementSpeed; break;
            case 4: rb2d.velocity = Vector2.zero; break;
            default: break;
        }

        await Task.Delay((int)(delay * 1000));
        rb2d.velocity = Vector2.zero;
        await Task.Delay((int)(delay * 1000));
        idleAction = false;
    }

    private void SpriteBalance()
    {
        if (characterMovement.isJumping && player.position.y < transform.position.y)
        {
            spriteRenderer.sortingOrder = -10;
        }
        else if (characterMovement.isJumping == false)
        {
            spriteRenderer.sortingOrder = 0;
        }
    }

    private void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * -Mathf.Sign(player.position.x - transform.position.x);
        transform.localScale = scale;
    }
}
