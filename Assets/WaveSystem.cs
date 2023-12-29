using System.Collections;
using UnityEngine;
using Cinemachine;
using System;
using UniRx;
using System.Threading.Tasks;
using System.Collections.Generic;

public class EnemyWaveSystem : MonoBehaviour
{
    [SerializeField] private Wave[] wave;
    [SerializeField] private GameObject[] cameraBorders;
    [SerializeField] private GameObject goSign;
    [SerializeField] private Transform enemyCameraPoint;
    private Wave currentWave;
    [SerializeField] private CinemachineVirtualCamera playerCamera;
    [SerializeField] private CinemachineVirtualCamera enemyCamera;
    [SerializeField] private Transform player;
    private int currentWaveNumber;
    public bool startWave;

    [Serializable]
    public class Wave
    {
        public string waveName;
        public List<EnemyAI> regularEnemy = new();
        public List<StrongEnemyAi> strongEnemy = new();
    }
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

            for (int i = 0; i < cameraBorders.Length; i++)
            {
                cameraBorders[i].SetActive(true);
            }
        }
    }

    private async void WaveEnd()
    {
        if ((currentWave.regularEnemy.Count + currentWave.strongEnemy.Count) <= 0 && startWave && currentWaveNumber < wave.Length)
        {
            goSign.SetActive(true);
            currentWaveNumber++;
            startWave = false;
            enemyCamera.Priority = 0;
            playerCamera.Priority = 10;
            await Task.Delay(2000);
            goSign.SetActive(false);
            enemyCamera.transform.position = enemyCameraPoint.position;
            for (int i = 0; i < cameraBorders.Length; i++)
            {
                cameraBorders[i].SetActive(false);
            }
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
