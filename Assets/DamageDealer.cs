using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] private BoxCollider2D damageDealerCollider;
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    CharacterDamage character = collision.GetComponent<CharacterDamage>();
    //    if (collision.CompareTag("Player"))
    //    {
    //        character.TakeDamage(damage);
    //    }
    //}

    private void Start()
    {
        damageDealerCollider.OnTriggerEnter2DAsObservable().Where(x => x.GetComponent<CharacterDamage>() != null).Subscribe(x =>
        {
            CharacterDamage characterDamage = x.GetComponent<CharacterDamage>();
            if (characterDamage != null)
            {
                characterDamage.TakeDamage(damage); // Call the TakeDamage method
            }
        });
    }
}

