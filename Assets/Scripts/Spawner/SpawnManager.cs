using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Transform[] spawners; 
    [SerializeField] private Transform basicEnemy, shooterEnemy;
    [SerializeField] private Wave[] waves;
    [SerializeField] private TextMeshProUGUI text;

    public int currWave = 0;
    public bool spawningWave = false;
    public static int totalEnemyCount = 0;
    
    [System.Serializable]
    struct Wave
    {
        public int basicEnemyNum, shooterEnemyNum;
        public Wave(int basicEnemyNum, int shooterEnemyNum)
        {
            this.basicEnemyNum = basicEnemyNum;
            this.shooterEnemyNum = shooterEnemyNum;
        }
    }

    private void Awake()
    {
        text.text = string.Format("Wave: {0}", currWave + 1);
        SpawnWave(waves[currWave]);
    }

    private void Update()
    {
        if (totalEnemyCount <= 0 && spawningWave == false)
        {
            totalEnemyCount = 0;
            NextWave();
        }
    }

    public void RestartWaves()
    {
        currWave = 0;
        spawningWave = false;
        text.text = string.Format("Wave: {0}", currWave + 1);
        totalEnemyCount = 0;
        SpawnWave(waves[0]);
    }

    private void NextWave()
    {
        currWave += 1;
        text.text = string.Format("Wave: {0}", currWave + 1);
        if (currWave < waves.Length - 1)
            SpawnWave(waves[currWave]);
        else
        {
            SpawnWave(waves[waves.Length - 1]);
        }
    }
    
    private void SpawnWave(Wave wave)
    {
        SpawnEnemies(wave.basicEnemyNum, wave.shooterEnemyNum);
    }
    
    private void SpawnEnemies(int basicEnemyNum, int shooterEnemyNum)
    {
        spawningWave = true;
        int basicSpawnedCount = basicEnemyNum, shooterSpawnedCount = shooterEnemyNum;
        for (int i = 0; i < basicEnemyNum + shooterEnemyNum; i++)
        {
            int randomSpawnerNum = Random.Range(0, spawners.Length - 1);
            Transform currSpawner = spawners[randomSpawnerNum];

            if (basicSpawnedCount > 0)
            {
                Instantiate(basicEnemy, currSpawner.position, currSpawner.rotation);
                basicSpawnedCount -= 1;
            } else if (shooterSpawnedCount > 0)
            {
                Instantiate(shooterEnemy, currSpawner.position, currSpawner.rotation);
                shooterSpawnedCount -= 1;
            }
        }
        spawningWave = false;
    }
}
