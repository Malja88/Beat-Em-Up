using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class EnemyScriptTEST : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private Rigidbody2D rb2d;

    [SerializeField] private float chasingSpeed;
    [SerializeField] private float followDistance;
    [SerializeField] private float attackDistance;
    [SerializeField] private float interval;
    [SerializeField] private float idleMovementSpeed;

    [SerializeField] public bool isFollowing;
    [SerializeField] public bool isIdle = false;
    [SerializeField] public bool idleAction = false;

    private void Start()
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
        });
    }

    private void SpriteBalance()
    {
        if (characterMovement.isJumping && player.position.y < transform.position.y)
        {
            spriteRenderer.sortingOrder = -10;                  
        }
        else if(characterMovement.isJumping == false)
        {
            spriteRenderer.sortingOrder = 0;
        }
    }

    private void FollowPlayer()
    {

        if (Vector2.Distance(transform.position, player.position) < followDistance)
        {
            isFollowing = true;
            isIdle = false;
            Vector2 direction = (player.position - transform.position).normalized;
            rb2d.velocity = direction * 1.5f;
        }

        else
        {
            isFollowing = false;
            isIdle = true;  
        }
    }

  
    private void Idle()
    {
        if (!isIdle && !isFollowing) { return; }
        if (!idleAction)
        {
            int randomAction = Random.Range(0, 4);
            float delay = 3;
            _ = PerformAction(randomAction, delay);
        }
        else
        {
            FollowPlayer();
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
    private void Flip()
    {
        Vector3 scale = transform.localScale;
        if (player.position != null)
        {
            if (player.position.x > transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x) * -1;
            }
            else if (player.position.x < transform.position.x)
            {
                scale.x = Mathf.Abs(scale.x);
            }
        }
        transform.localScale = scale;
    }


}
