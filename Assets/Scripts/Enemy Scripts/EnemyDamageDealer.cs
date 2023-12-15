using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] private BoxCollider2D damageDealerCollider;

    private void Start()
    {
        damageDealerCollider.OnTriggerEnter2DAsObservable().Where(x => x.GetComponent<CharacterHealth>() != null).Subscribe(x =>
        {
            if (x.TryGetComponent<CharacterHealth>(out var characterDamage))
            {
                characterDamage.TakeDamage(damage);
            }
        });
    }
}

