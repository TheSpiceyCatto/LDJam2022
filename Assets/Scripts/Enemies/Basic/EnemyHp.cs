using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    [SerializeField] private int hp = 5;
    [SerializeField, Range(1, 100)] private int healAmount = 5;

    private PlayerHp playerHp;
    
    private void Awake()
    {
        playerHp = FindObjectOfType<PlayerHp>();
        SpawnManager.totalEnemyCount += 1;
    }

    public void DealDamage()
    {
        hp -= 1;
        if (hp <= 0)
        {
            playerHp.GainHp(healAmount);
            SpawnManager.totalEnemyCount -= 1;
            Destroy(gameObject);
        }
    }
}
