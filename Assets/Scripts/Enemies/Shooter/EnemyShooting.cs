using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private Transform bullet;
    [SerializeField] private Transform shootPoint;
    [SerializeField, Range(0f, 180f)] private float rotationSpeed;
    [SerializeField] private float fireRate;
    
    private bool canShoot = true;
    private Transform player;
    //private Vector3 playerMove;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        RotateToPlayer();
        if (canShoot)
        {
            StartCoroutine(FireRate(fireRate));
            ShootPlayer();
        }
    }

    private void RotateToPlayer()
    {
        //playerMove = player.GetComponent<PlayerMove>().desiredVelocity;
        Vector2 enemyPos2 = new Vector2(transform.position.x, transform.position.z);
        Vector2 playerPos2 = new Vector2(player.position.x, player.position.z);
        //Vector2 offsetPlayerPos = new Vector2(playerPos2.x + playerDir.x * playerSpeed, playerPos2.y + playerDir.y * playerSpeed);
        Vector2 dirToPlayer = (playerPos2 - enemyPos2).normalized;
        Debug.DrawLine(transform.position, new Vector3(dirToPlayer.x, 0f, dirToPlayer.y), Color.red);
        Quaternion rotToAng = Quaternion.LookRotation(new Vector3(dirToPlayer.x, 0f, dirToPlayer.y));
        var rotationAccel = Quaternion.RotateTowards(transform.rotation, rotToAng, rotationSpeed);
        transform.rotation = rotationAccel;
    }

    private void ShootPlayer()
    {
        Instantiate(bullet, shootPoint.position, shootPoint.rotation);
    }
    
    private IEnumerator FireRate(float fireRate)
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
