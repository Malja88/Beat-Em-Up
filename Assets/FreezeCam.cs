using UnityEngine;
using Cinemachine;

public class FreezeCamera : MonoBehaviour
{
    public EnemyWaveSystem waveSystem;


    void Start()
    {
  
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            waveSystem.startWave = true;
        }
    }
}
