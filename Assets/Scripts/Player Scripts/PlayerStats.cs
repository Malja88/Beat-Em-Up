using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [SerializeField] public float maxHealth;
    [SerializeField] public float horizontalSpeed;
    [SerializeField] public float verticalSpeed;
    [SerializeField] public float skillPoints;
    [SerializeField] public float damage;
    [SerializeField] public float defence;
}
