using DG.Tweening;
using TMPro;
using UnityEngine;

public class SkillPointsNumberScript : MonoBehaviour
{
    [SerializeField] private EnemyDamage enemyDamage;
    [SerializeField] private TextMeshProUGUI numberText;
    private void Awake()
    {
        enemyDamage = FindObjectOfType<EnemyDamage>();
    }
    void Start()
    {
        numberText.text = enemyDamage.skillPoint.ToString();
        float currentY = transform.position.y;
        transform.DOMoveY(currentY + 1.2f, 1);
        Destroy(gameObject, 1f);
    }
}
