using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Restart : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject mesh;
    [SerializeField] private TextMeshProUGUI hpText;
    private GameObject currPlayer;
    private SpawnManager spawnManager;
    private bool canRestart = true;

    private void Awake()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        currPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void OnEscape()
    {
        Debug.Log("Escape");
        Application.Quit();
    }

    void OnRestart(InputValue value)
    {
        if (canRestart && value.isPressed)
        {
            canRestart = false;
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies != null)
            {
                foreach (var enemy in enemies)
                {
                    Destroy(enemy);
                }
            }

            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullets");
            if (bullets != null)
            {
                foreach (var bullet in bullets)
                {
                    Destroy(bullet);
                }
            }
            
            GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
            if (enemyBullets != null)
            {
                foreach (var eBullet in enemyBullets)
                {
                    Destroy(eBullet);
                }
            }
            
            mesh.SetActive(true);
            currPlayer.GetComponent<CapsuleCollider>().enabled = true;
            currPlayer.GetComponent<PlayerMove>().enabled = true;
            currPlayer.GetComponent<PlayerShoot>().enabled = true;
            currPlayer.GetComponent<PlayerInput>().enabled = true;
            currPlayer.GetComponent<PlayerHp>().enabled = true;
            currPlayer.GetComponent<PlayerHp>().hp = 100;
            hpText.text = "HP: 100";
            currPlayer.GetComponent<Transform>().position = new Vector3(0f, 1f, 0f);
            currPlayer.GetComponent<Transform>().rotation = Quaternion.identity;
            gun.SetActive(true);
            spawnManager.RestartWaves();
            canRestart = true;
        }
    }
}
