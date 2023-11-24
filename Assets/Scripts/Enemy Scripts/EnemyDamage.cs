using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private PlayerLevelUpSystem playerLevelUpSystem;
    [SerializeField] private Transform player;
    [SerializeField] private Transform skillpointStartFlight;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private CharacherController characherController;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float punchRecoilForce;
    [SerializeField] private float stunnedTime;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject skillPointPrefab;
    [SerializeField] private int coinAmount;
    public int skillPoint;

    void Start()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
            skillPoint = Random.Range(1, 6);
            //StunnedAsync();
        });

        boxCollider.OnTriggerEnter2DAsObservable().Subscribe(_ =>
        {
            if(_.CompareTag("WeakAttack"))
            {
                //rb2d.AddForce(Vector2.right * 2, ForceMode2D.Impulse);
                //enemyAI.isIdle = false;
                //enemyAI.isAttacking = false;
                CoinSplash();
            }
            if(_.CompareTag("Punch"))
            {
                playerLevelUpSystem.GainExperience(skillPoint);
                Instantiate(skillPointPrefab, skillpointStartFlight.position, Quaternion.identity);
                KnockBackAsync();            
            }
        });

        boxCollider.OnCollisionEnter2DAsObservable().Subscribe(_ => 
        {
            if (_.gameObject.CompareTag("Pipe"))
            {
                rb2d.velocity = Vector2.zero;
            }
        });
    }

    //public async Task StunnedAsync()
    //{
    //    if (characherController.isHit)
    //    {
    //        //KnockBack();
    //        rb2d.velocity = Vector2.zero;
    //       // enemyAI.isIdle = false;
    //       //enemyAI.canAttack = false;
    //       // await Task.Delay(1000);
    //        //enemyAI.isIdle = true;
    //        //enemyAI.canAttack = true;
    //    }
    //}

    private async void KnockBackAsync()
    {
        if (player.position.x < transform.position.x)
        {
            rb2d.AddForce(Vector2.right * punchRecoilForce, ForceMode2D.Impulse);
            await Task.Delay(200);
            rb2d.velocity = Vector2.zero;
        }
        if (player.position.x > transform.position.x)
        {
            rb2d.AddForce(Vector2.left * punchRecoilForce, ForceMode2D.Impulse);
            await Task.Delay(200);
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
