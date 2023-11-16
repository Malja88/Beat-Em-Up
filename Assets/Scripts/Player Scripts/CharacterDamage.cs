using UniRx.Triggers;
using UnityEngine;
using UniRx;

public class CharacterDamage : MonoBehaviour
{
    [SerializeField] private new BoxCollider2D collider;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerStats stats;
    GlobalStringVariables variables = new GlobalStringVariables();
    private float currentHealth;
    void Start()
    {
        currentHealth = stats.maxHealth;
        collider.OnTriggerEnter2DAsObservable().Where(x => x.CompareTag("Damage")).Subscribe(_ => 
        {
            
        });
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.Play(variables.HurtHash);
    }
}
