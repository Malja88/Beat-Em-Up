using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private new Collider2D collider2D;
    void Start()
    {
        collider2D.OnTriggerEnter2DAsObservable().Subscribe(_ => 
        { 
            if(_.CompareTag("WeakAttack"))
            {
                Debug.Log("Hello");
            }
        });
    }
}
