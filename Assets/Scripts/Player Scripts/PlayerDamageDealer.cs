using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class PlayerDamageDealer : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] private BoxCollider2D playerDamageDealerCollider;
    private void Awake()
    {
        playerDamageDealerCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        playerDamageDealerCollider.OnTriggerEnter2DAsObservable().Where(x => x.GetComponent<EnemyHealth>() != null).Subscribe(x =>
        {
            if (x.TryGetComponent<EnemyHealth>(out var enemyHealth))
            {
                enemyHealth.TakeDamage(damage);
            }
        });
    }
}
