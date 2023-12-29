using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [HideInInspector] private EnemyAI enemyAI;
    [Header("Body Components")]
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Animator animator;

    [Header("Recoil Values")]
    [SerializeField] private float punchRecoilForce;
    [SerializeField] private float knockDownRecoilForce;
    [SerializeField] private float knockDownForceByWeapon;

    [Header("Bonuses & Effects")]
    [SerializeField] private Transform skillpointStartFlight;
    [SerializeField] private ParticleSystem knockDownEffect;
    [SerializeField] private ParticleSystem regularBloodSplash;
    [SerializeField] private ParticleSystem knockDownBloodSplash;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject skillPointPrefab;
    [SerializeField] private int coinAmount;

    [SerializeField] private Transform player;
    readonly GlobalStringVariables variables = new();

    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
    }
    void Start()
    {
          boxCollider.OnTriggerEnter2DAsObservable().Subscribe(_ =>
        {
            if(_.CompareTag(variables.KnockdownPunchTag))
            {
                //knockDownEffect.Play();
                knockDownBloodSplash.Play();
                animator.Play(variables.EnemyKnockDown);
                KnockBack(knockDownRecoilForce);
                CoinSplash();
            }
            if(_.CompareTag(variables.PunchTag))
            {
                animator.Play(variables.EnemyHurt);
                regularBloodSplash.Play();
                Instantiate(skillPointPrefab, skillpointStartFlight.position, Quaternion.identity);
                KnockBack(punchRecoilForce);            
            }
            if(_.CompareTag(variables.WeaponTag))
            {
                //knockDownEffect.Play();
                knockDownBloodSplash.Play();
                animator.Play(variables.EnemyKnockDown);
                KnockBack(knockDownForceByWeapon);
                CoinSplash();
            }
        });

        boxCollider.OnCollisionEnter2DAsObservable().Where(x => x.gameObject.CompareTag(variables.WallTag)).Subscribe(_ => { rb2d.velocity = Vector2.zero; });
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
            enemyAI.isIdle=true;
            rb2d.velocity = Vector2.zero;
            
        }
        else
        {
            enemyAI.canAttack = false;
            enemyAI.isIdle = false;
            rb2d.AddForce(Vector2.left * punchPower, ForceMode2D.Impulse);
            await Task.Delay(300);
            enemyAI.canAttack = true;
            enemyAI.isIdle=true;
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
