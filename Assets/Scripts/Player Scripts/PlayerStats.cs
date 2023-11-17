using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] public float maxHealth;
    [SerializeField] public float horizontalSpeed;
    [SerializeField] public float verticalSpeed;
    [SerializeField] public int skillPoints;
    [SerializeField] public int level;
    [SerializeField] public int damage;
    [SerializeField] public float defence;
}
