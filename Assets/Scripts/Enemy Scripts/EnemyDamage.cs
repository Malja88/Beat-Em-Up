using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private CharacherController characherController;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float punchRecoilForce;
    [SerializeField] private float stunnedTime;
    [SerializeField] public bool isStunned;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject skillPointPrefab;
    [SerializeField] private int coinAmount;
    [SerializeField] private int skillPoint;
    void Start()
    {
        Observable.EveryUpdate().Subscribe(_ =>
        {
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
                // Доработать!!!
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

    //private void KnockBack()
    //{
    //    if (player.position.x < transform.position.x)
    //    {
    //        rb2d.AddForce(Vector2.right * punchRecoilForce, ForceMode2D.Impulse);
    //    }
    //    if (player.position.x > transform.position.x)
    //    {
    //        rb2d.AddForce(Vector2.left * punchRecoilForce, ForceMode2D.Impulse);
    //    }
       
    //}

    private void CoinSplash()
    {
        for (int i = 0; i < coinAmount; i++)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }      
    }
}
