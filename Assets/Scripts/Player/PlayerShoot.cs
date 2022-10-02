using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private Transform bullet;
    [SerializeField] private Transform shootPoint;
    [SerializeField, Min(1f)] private int maxBulletCount;
    [SerializeField, Range(0.1f, 1f)] private float fireRate;
    [SerializeField, Range(1f, 180f)] private float rotationSpeed = 1f;

    private bool isShooting, canShoot;
    private Vector2 mousePos;
    
    void OnMousePos(InputValue value)
    {
        mousePos = value.Get<Vector2>();
    }

    void OnMouseClick(InputValue value)
    {
        isShooting = value.isPressed;
    }

    private void Awake()
    {
        canShoot = true;
    }

    private void FixedUpdate()
    {
        RotateToMouse();
        if (isShooting && canShoot && StandardBullet.bulletCount <= maxBulletCount)
        {
            StartCoroutine(FireRate(fireRate));
            Shoot();
        }
    }

    private void RotateToMouse()
    {
        var mouseRay = mainCam.ScreenPointToRay(mousePos);
        var startPos = transform.position;
        var p = new Plane(Vector3.up, startPos);
        if (!p.Raycast(mouseRay, out var hitDist))
        {
            return;
        }
        var mouseWorldPos = mouseRay.GetPoint(hitDist);
        var dirToMouse = mouseWorldPos - startPos;
        Quaternion rotToAng = Quaternion.LookRotation(dirToMouse);
        var rotationAccel = Quaternion.RotateTowards(transform.rotation, rotToAng, rotationSpeed);
        transform.rotation = rotationAccel;
    }

    private void Shoot()
    {
        Transform currentBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        //currentBullet.SetParent(gun, true);
    }

    private IEnumerator FireRate(float fireRate)
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }
}
