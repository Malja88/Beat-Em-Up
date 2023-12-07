using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] public float currentHealth;
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D enemyCollider;
    [SerializeField] private Rigidbody2D rb;
    readonly GlobalStringVariables variables = new();
    void Awake()
    {
        enemyCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= damage && currentHealth <= 0)
        {
            EnemyDeath();
        }
    }

    public virtual void EnemyDeath()
    {
        rb.velocity = Vector2.zero;
        enemyCollider.enabled = false;
        animator.Play(variables.EnemyDeath);
        Destroy(gameObject,1.8f);
    }
}
