using UnityEngine;
using UnityEngine.UI;

public class PlayerUITest : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private CharacterDamage character;
    [SerializeField] private Image image;
    void Start()
    {
        slider.maxValue = character.currentHealth;
        slider.value = character.currentHealth;
        image.fillAmount = character.currentHealth;
    }

    public void HealthBarDamage(int damage)
    {
        slider.value -= damage;
    }
}
