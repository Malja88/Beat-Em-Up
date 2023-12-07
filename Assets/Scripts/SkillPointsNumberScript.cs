using DG.Tweening;
using TMPro;
using UnityEngine;

public class SkillPointsNumberScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI numberText;
    [SerializeField] private PlayerLevelUpSystem playerLevelUpSystem;
    [HideInInspector] public int skillPoint;
    private void Awake()
    {
         playerLevelUpSystem = FindObjectOfType<PlayerLevelUpSystem>();
         skillPoint = Random.Range(1, 6);
    }
    void Start()
    {
      
        playerLevelUpSystem.GainExperience(skillPoint = Random.Range(1, 6));
        numberText.text = skillPoint.ToString();
        float currentY = transform.position.y;

        if (!DOTween.IsTweening(transform))
        {
            transform.DOMoveY(currentY + 1.2f, 1).OnKill(() => Destroy(gameObject));
        }
        else
        {
            Destroy(gameObject, 1f);
        }
    }
}
