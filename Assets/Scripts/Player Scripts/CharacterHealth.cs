using UniRx.Triggers;
using UnityEngine;
using UniRx;
using System.Threading.Tasks;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private float recoilPower;
    [Header("Body Components")]
    [SerializeField] private new BoxCollider2D collider;
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Animator animator;

    [Header("Player Stats + UI Interactable")]
    [SerializeField] private PlayerStats stats;
    [SerializeField] private PlayerUITest test;

    [HideInInspector] public float currentHealth;
    readonly GlobalStringVariables variables = new();

    void Awake()
    {
        currentHealth = stats.maxHealth;
        collider.OnTriggerEnter2DAsObservable().Where(x => x.CompareTag(variables.DamageTag)).Subscribe(_ => 
        {
            ApplyRecoilOnHit();
        });
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        test.HealthBarDamage(damage);
        animator.Play(variables.HurtHash);
    }

    private async void ApplyRecoilOnHit()
    {      
        if (transform.rotation.y < 0)
        {
            rb2d.AddForce(Vector2.right * recoilPower, ForceMode2D.Impulse);
            await Task.Delay(500);
            rb2d.velocity = Vector2.zero;
        }
        else
        {
            rb2d.AddForce(Vector2.left * recoilPower, ForceMode2D.Impulse);
            await Task.Delay(500);
            rb2d.velocity = Vector2.zero;
        }
    }
}
