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
            }
        });     
    }

    private void SpawnWave()
    {
        if (currentWave != null && startWave)
        {
            enemyCamera.Priority = 10;
            playerCamera.Priority = 0;

            // Spawn regular enemies
            for (int i = 0; i < currentWave.regularEnemy.Count; i++)
            {
                if (currentWave.regularEnemy[i] != null)
                {
                    currentWave.regularEnemy[i].enabled = true;
                }
            }

            // Spawn strong enemies
            for (int i = 0; i < currentWave.strongEnemy.Count; i++)
            {
                if (currentWave.strongEnemy[i] != null)
                {
                    currentWave.strongEnemy[i].enabled = true;
                }
            }
        }
    }

    private void WaveEnd()
    {
        if ((currentWave.regularEnemy.Count + currentWave.strongEnemy.Count) <= 0 && startWave && currentWaveNumber < wave.Length)
        {
            currentWaveNumber++;
            startWave = false;
            enemyCamera.Priority = 0;
            playerCamera.Priority = 10;
        }
    }

    public void EnemyDefeated(EnemyAI enemy)
    {
        if (currentWave.regularEnemy.Contains(enemy))
        {
            currentWave.regularEnemy.Remove(enemy);
        }
        else if (currentWave.strongEnemy.Contains(enemy as StrongEnemyAi))
        {
            currentWave.strongEnemy.Remove(enemy as StrongEnemyAi);
        }
    }
}
