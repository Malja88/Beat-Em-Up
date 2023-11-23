using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUITest : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider skillSlider;
    [SerializeField] private CharacterDamage character;
    [SerializeField] private PlayerLevelUpSystem playerLevelUpSystem;
    void Start()
    {
        healthSlider.maxValue = character.currentHealth;
        healthSlider.value = character.currentHealth;

        skillSlider.maxValue = playerLevelUpSystem.skillPointsToNextLevel;
        skillSlider.value = playerLevelUpSystem.currentSkillPoints;

        Observable.EveryUpdate().Subscribe(x =>
        {
            skillSlider.value = playerLevelUpSystem.currentSkillPoints;
            skillSlider.maxValue = playerLevelUpSystem.skillPointsToNextLevel;
        });
    }

    public void HealthBarDamage(int damage)
    {
        healthSlider.value -= damage;
    }
}
