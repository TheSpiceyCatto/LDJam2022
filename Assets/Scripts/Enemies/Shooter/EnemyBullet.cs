using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float bulletSpeed = 5f;
    [SerializeField, Range(1f, 60f)] private float maxLifeTime = 10f;

    private Rigidbody bulletBody;

    private void Awake()
    {
        bulletBody = GetComponent<Rigidbody>();
        StartCoroutine(Die(maxLifeTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            DestroyBullet();
        if (collision.collider.CompareTag("Wall"))
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

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
