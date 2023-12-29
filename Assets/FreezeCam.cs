using UnityEngine;
using Cinemachine;

public class FreezeCamera : MonoBehaviour
{
    [SerializeField] private EnemyWaveSystem waveSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            waveSystem.startWave = true;
            gameObject.SetActive(false);
        }
    }
}
