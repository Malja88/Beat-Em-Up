using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class StrongEnemyDamage : MonoBehaviour
{
    [SerializeField] private BoxCollider2D playerCollider;
    [HideInInspector] private StrongEnemyAi enemyAI;
    [SerializeField] private PlayerLevelUpSystem playerLevelUpSystem;
    [SerializeField] private Transform player;
    [SerializeField] private Transform skillpointStartFlight;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private CharacherController characherController;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem knockDownEffect;
    [SerializeField] private float punchRecoilForce;
    [SerializeField] private float knockDownRecoilForce;
    [SerializeField] private float knockDownForceByWeapon;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject skillPointPrefab;
    [SerializeField] private int coinAmount;
    readonly GlobalStringVariables variables = new();

    private void Awake()
    {
        enemyAI = GetComponent<StrongEnemyAi>();
    }
    void Start()
    {
        playerCollider.OnTriggerEnter2DAsObservable().Subscribe(_ =>
        {
            if (_.CompareTag("WeakAttack"))
            {
                knockDownEffect.Play();
                animator.Play(variables.EnemyKnockDown);
                KnockBack(knockDownRecoilForce);
                CoinSplash();
            }
            if (_.CompareTag("Punch"))
            {
                animator.Play(variables.EnemyHurt);
                Instantiate(skillPointPrefab, skillpointStartFlight.position, Quaternion.identity);
                KnockBack(punchRecoilForce);
            }
            if (_.CompareTag("Weapon"))
            {
                KnockBackByWeapon();
            }
        });

        playerCollider.OnCollisionEnter2DAsObservable().Where(x => x.gameObject.CompareTag("Wall")).Subscribe(_ => { rb2d.velocity = Vector2.zero; });
    }

    private async void KnockBack(float punchPower)
    {
        if (player.position.x < transform.position.x)
        {
            enemyAI.canAttack = false;
            enemyAI.isIdle = false;
            rb2d.AddForce(Vector2.right * punchPower, ForceMode2D.Impulse);
            await Task.Delay(300);
            enemyAI.canAttack = true;
            enemyAI.isIdle = true;
            rb2d.velocity = Vector2.zero;
            
        }
        else
        {
            enemyAI.canAttack = false;
            enemyAI.isIdle = false;
            rb2d.AddForce(Vector2.left * punchPower, ForceMode2D.Impulse);
            await Task.Delay(300);
            enemyAI.canAttack = true;
            enemyAI.isIdle = true;
            rb2d.velocity = Vector2.zero;
        }
    }

    private void CoinSplash()
    {
        for (int i = 0; i < coinAmount; i++)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }

    private async void KnockBackByWeapon()
    {
        if (player.position.x < transform.position.x)
        {
            rb2d.AddForce(Vector2.right * knockDownForceByWeapon, ForceMode2D.Impulse);
            knockDownEffect.Play();
            animator.Play(variables.EnemyKnockDown);
            await Task.Delay(100);
            rb2d.velocity = Vector2.zero;
        }
        else
        {
            rb2d.AddForce(Vector2.left * knockDownForceByWeapon, ForceMode2D.Impulse);
            knockDownEffect.Play();
            animator.Play(variables.EnemyKnockDown);
            await Task.Delay(100);
            rb2d.velocity = Vector2.zero;
        }
    }
}
