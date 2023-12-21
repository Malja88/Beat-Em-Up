using System.Collections;
using UnityEngine;
using Cinemachine;
using System;
using UniRx;

public class EnemyWaveSystem : MonoBehaviour
{
    [SerializeField] private Wave[] wave;
    private Wave currentWave;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private CinemachineVirtualCamera enemyCamera;
    [SerializeField] private Transform player;
    private int currentWaveNumber;
    public bool startWave;

    private void Start()
    {
        Observable.EveryUpdate().Subscribe(_ => 
        {
            if (currentWaveNumber < wave.Length)
            {
                currentWave = wave[currentWaveNumber];
                SpawnWave();
                WaveEnd();
                Debug.Log(currentWaveNumber);
            }

        });

        
    }

    private void SpawnWave()
    {
        if (currentWave != null && currentWave.typeOfEnemy != null && startWave)
        {
            //playerCamera.Follow = null;
            enemyCamera.Priority = 10;
            playerCamera.Priority = 0;
            for (int i = 0; i < currentWave.typeOfEnemy.Length; i++)
            {
                if (currentWave.typeOfEnemy[i] != null)
                {
                    currentWave.typeOfEnemy[i].enabled = true;
                }
            }
        }
    }

    private void WaveEnd()
    {
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length <= 0 && startWave && currentWaveNumber < wave.Length)
        {
            currentWaveNumber++;
            startWave = false;
            //playerCamera.Follow = player;
            enemyCamera.Priority = 0;
            playerCamera.Priority = 10;
        }
    }
}
