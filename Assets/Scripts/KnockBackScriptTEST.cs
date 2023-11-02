using System.Threading.Tasks;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class KnockBackScriptTEST : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Animator animator;
    void Start()
    {
        boxCollider.OnTriggerEnter2DAsObservable().Subscribe(_ => 
        { 
            if(_.gameObject.CompareTag("Enemy"))
            {
                //BeingKnocked();
            }
        }) ;
    }

    private void BeingKnocked()
    {
        rb.AddForce(new Vector2(-2, 0) * 4, ForceMode2D.Impulse);
        animator.Play("Jump");
    }
}
