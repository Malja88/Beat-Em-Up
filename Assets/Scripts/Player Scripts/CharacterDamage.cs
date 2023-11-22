using UniRx.Triggers;
using UnityEngine;
using UniRx;

public class CharacterDamage : MonoBehaviour
{
    [SerializeField] private new BoxCollider2D collider;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private PlayerUITest test;
    GlobalStringVariables variables = new GlobalStringVariables();
    public float currentHealth;
    void Awake()
    {
        currentHealth = stats.maxHealth;
        collider.OnTriggerEnter2DAsObservable().Where(x => x.CompareTag("Damage")).Subscribe(_ => 
        {
            
        });
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        test.HealthBarDamage(damage);
        animator.Play(variables.HurtHash);
    }
}
