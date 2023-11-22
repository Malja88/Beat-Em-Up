using TMPro;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLevelUpSystem : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private int playerCurrentLevel;
    [SerializeField] private int skillPoints;
    [SerializeField] private int skillPointsToNextLevel;
    [SerializeField] private BoxCollider2D playerCollider;
    private float currentCoinPrice;
    [SerializeField] private TextMeshProUGUI coinText;
    void Start()
    {
        playerCollider.OnTriggerEnter2DAsObservable().Where(x => x.CompareTag("Coin")).Subscribe(_ =>
        {
            playerStats.coins += currentCoinPrice;
        });

        Observable.EveryUpdate().Subscribe(_ =>
        {
            coinText.text = playerStats.coins.ToShortString(3);
        });

        currentCoinPrice = Random.Range(0.1f, 2);
        coinText.text = playerStats.coins.ToShortString(3);
        playerCurrentLevel = playerStats.level;   
        skillPoints = playerStats.skillPoints;
    }
    public void GainExperience(int amount)
    {
        skillPoints += amount;
        if (skillPoints >= skillPointsToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        playerCurrentLevel++;
        skillPoints = 0;
        //skillPointsToNextLevel rise from new level
    }
}
