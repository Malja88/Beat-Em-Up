using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHealth;
    [HideInInspector] public float currentHealth;

    [Header("Body Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D enemyCollider;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private EnemyAI enemyAI;

    readonly GlobalStringVariables variables = new();
    private EnemyWaveSystem enemyWaveSystem;
    void Awake()
    {
        enemyCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        enemyAI = GetComponent<EnemyAI>();
        currentHealth = maxHealth;
        enemyWaveSystem = FindObjectOfType<EnemyWaveSystem>();
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
        rb.bodyType = RigidbodyType2D.Static;
        enemyAI.isFlip = false;
        enemyCollider.enabled = false;
        animator.Play(variables.EnemyDeath);
        Destroy(gameObject,1.8f);
        if (enemyWaveSystem != null)
        {
            enemyWaveSystem.EnemyDefeated(GetComponent<EnemyAI>());
        }
    }
}
