using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBullet : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float bulletSpeed = 5f;
    [SerializeField, Range(1f, 60f)] private float maxLifeTime = 10f;

    public static int bulletCount = 0;
    private Rigidbody bulletBody;

    private void Awake()
    {
        bulletBody = GetComponent<Rigidbody>();
        bulletCount += 1;
        StartCoroutine(Die(maxLifeTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
            collision.collider.GetComponent<EnemyHp>().DealDamage();
            DestroyBullet();
        if (collision.collider.CompareTag("Wall") == true)
            DestroyBullet();
    }

    private void FixedUpdate()
    {
        bulletBody.velocity = transform.TransformDirection(Vector3.forward * bulletSpeed);
    }
    
    IEnumerator Die(float time)
    {
        yield return new WaitForSeconds(time);
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        bulletCount -= 1;
        Destroy(gameObject);
    }
}
