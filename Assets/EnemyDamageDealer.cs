using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] private BoxCollider2D damageDealerCollider;

    private void Start()
    {
        damageDealerCollider.OnTriggerEnter2DAsObservable().Where(x => x.GetComponent<CharacterHealth>() != null).Subscribe(x =>
        {
            CharacterHealth characterDamage = x.GetComponent<CharacterHealth>();
            if (characterDamage != null)
            {
                characterDamage.TakeDamage(damage);
            }
        });
    }
}

