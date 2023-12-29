using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class StrongEnemyDamage : MonoBehaviour
{
    [HideInInspector] private StrongEnemyAi strongEnemyAI;
    [SerializeField] private Transform player;
    [Header("Body Components")]
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;

    [Header("Recoil Values")]
    [SerializeField] private float punchRecoilForce;
    [SerializeField] private float knockDownRecoilForce;
    [SerializeField] private float knockDownForceByWeapon;

    [Header("Bonuses & Effects")]
    [SerializeField] private Transform skillpointStartFlight;
    [SerializeField] private ParticleSystem knockDownEffect;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject skillPointPrefab;
    [SerializeField] private int coinAmount;
    readonly GlobalStringVariables variables = new();

    private void Awake()
    {
        strongEnemyAI = GetComponent<StrongEnemyAi>();
    }
    void Start()
    {
        playerCollider.OnTriggerEnter2DAsObservable().Subscribe(_ =>
        {
            if (_.CompareTag(variables.KnockdownPunchTag))
            {
                knockDownEffect.Play();
                animator.Play(variables.EnemyKnockDown);
                KnockBack(knockDownRecoilForce);
                CoinSplash();
            }
            if (_.CompareTag(variables.PunchTag))
            {
                animator.Play(variables.EnemyHurt);
                Instantiate(skillPointPrefab, skillpointStartFlight.position, Quaternion.identity);
                KnockBack(punchRecoilForce);
            }
            if (_.CompareTag(variables.WeaponTag))
            {
                knockDownEffect.Play();
                animator.Play(variables.EnemyKnockDown);
                KnockBack(knockDownForceByWeapon);
                CoinSplash();
            }
        });

        playerCollider.OnCollisionEnter2DAsObservable().Where(x => x.gameObject.CompareTag(variables.WallTag)).Subscribe(_ => { rb2d.velocity = Vector2.zero; });
    }

    private async void KnockBack(float punchPower)
    {
        if (player.position.x < transform.position.x)
        {
            strongEnemyAI.timer = 0;
            strongEnemyAI.canAttack = false;
            strongEnemyAI.isIdle = false;
            rb2d.AddForce(Vector2.right * punchPower, ForceMode2D.Impulse);
            await Task.Delay(300);
            strongEnemyAI.canAttack = true;
            strongEnemyAI.isIdle = true;
            rb2d.velocity = Vector2.zero;           
        }
        else
        {
            strongEnemyAI.timer = 0;
            strongEnemyAI.canAttack = false;
            strongEnemyAI.isIdle = false;
            rb2d.AddForce(Vector2.left * punchPower, ForceMode2D.Impulse);
            await Task.Delay(300);
            strongEnemyAI.canAttack = true;
            strongEnemyAI.isIdle = true;
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
}
