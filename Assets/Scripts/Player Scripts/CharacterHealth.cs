using UniRx.Triggers;
using UnityEngine;
using UniRx;
using System.Threading.Tasks;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private float recoilPower;
    [SerializeField] private float knockDownRecoilPower;
    [SerializeField] private CharacterMovement movement;
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
        movement = GetComponent<CharacterMovement>();
        currentHealth = stats.maxHealth;
        collider.OnTriggerEnter2DAsObservable().Where(x => x.CompareTag(variables.DamageTag)).Subscribe(_ => {ApplyRecoilOnHit();});

        collider.OnTriggerEnter2DAsObservable().Where(x => x.CompareTag(variables.HeavyAttack)).Subscribe(_ => { KnockDownAsync(); });
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
            movement.isMoving = true;
            rb2d.AddForce(Vector2.right * recoilPower, ForceMode2D.Impulse);
            await Task.Delay(500);
            rb2d.velocity = Vector2.zero;
        }
        else
        {
            movement.isMoving = true;
            rb2d.AddForce(Vector2.left * recoilPower, ForceMode2D.Impulse);
            await Task.Delay(500);
            rb2d.velocity = Vector2.zero;
        }
    }

    private async void KnockDownAsync()
    {
        animator.Play(variables.KnockDownHash);
        if (transform.rotation.y < 0)
        {
            movement.isMoving = true;
            rb2d.AddForce(Vector2.right * knockDownRecoilPower, ForceMode2D.Impulse);
            await Task.Delay(500);
            rb2d.velocity = Vector2.zero;
        }
        else
        {
            movement.isMoving = true;
            rb2d.AddForce(Vector2.left * knockDownRecoilPower, ForceMode2D.Impulse);
            await Task.Delay(500);
            rb2d.velocity = Vector2.zero;
        }
    }
}
