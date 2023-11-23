using TMPro;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLevelUpSystem : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    public int currentSkillPoints;
    public float skillPointsToNextLevel;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private float currentCoinPrice;
    [SerializeField] private TextMeshProUGUI coinText;
    private void Awake()
    {
        currentCoinPrice = Random.Range(0.1f, 2);
        coinText.text = playerStats.coins.ToShortString(3);
    }
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
    }

    public void GainExperience(int amount)
    {
        currentSkillPoints += amount;
        if (currentSkillPoints >= skillPointsToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        playerStats.level++;
        currentSkillPoints = 0;
        skillPointsToNextLevel *= 1.2f;
    }
}
