using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyNav : MonoBehaviour
{
    [SerializeField] private float backAwayDistance;
    private Transform player;
    private NavMeshAgent navMesh;
    private Vector2 reverseDir, dirToPlayer;
    private float distanceToPlayer;
    private bool backAim;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        navMesh = GetComponent<NavMeshAgent>();
        backAim = Random.Range(0, 2) == 0 ? false : true;
    }

    private void FixedUpdate()
    {
        Vector2 enemyPos2 = new Vector2(transform.position.x, transform.position.z);
        Vector2 playerPos2 = new Vector2(player.position.x, player.position.z); 
        distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > backAwayDistance)
            navMesh.destination = player.position;
        else
        {
            dirToPlayer = backAim ? (playerPos2 - enemyPos2).normalized : (playerPos2 - enemyPos2).normalized * -1;
            navMesh.destination = new Vector3(dirToPlayer.x * backAwayDistance, 1f, dirToPlayer.y * backAwayDistance);
        }
    }
}
