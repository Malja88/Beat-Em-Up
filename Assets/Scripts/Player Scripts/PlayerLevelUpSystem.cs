using UnityEngine;

public class PlayerLevelUpSystem : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private int playerCurrentLevel;
    [SerializeField] private int skillPoints;
    [SerializeField] private int skillPointsToNextLevel;
    void Start()
    {
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
    }
}
