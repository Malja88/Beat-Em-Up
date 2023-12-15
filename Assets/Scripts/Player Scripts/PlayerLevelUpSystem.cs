using TMPro;
using UniRx;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLevelUpSystem : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private BoxCollider2D playerCollider;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] public int currentSkillPoints;
    [SerializeField] public float skillPointsToNextLevel;
    [HideInInspector] private float currentCoinPrice;

    readonly GlobalStringVariables variables = new();
    private void Awake()
    {
        currentCoinPrice = Random.Range(0.1f, 2);
        coinText.text = playerStats.coins.ToShortString(3);
    }
    void Start()
    {
        playerCollider.OnTriggerEnter2DAsObservable().Where(x => x.CompareTag(variables.CoinTag)).Subscribe(_ =>
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
