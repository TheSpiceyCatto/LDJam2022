using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField, Range(0f, 20f)] private float maxSpeed = 5f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 20f;
    
    
    private Vector2 moveDir;
    private Vector3 velocity;
    public Vector3 desiredVelocity;

    void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
        moveDir = Vector2.ClampMagnitude(moveDir, 1);
    }

    private void Update()
    {
        desiredVelocity =
            new Vector3(moveDir.x, 0f, moveDir.y) * maxSpeed;
    }

    private void FixedUpdate()
    {
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z = Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        body.velocity = velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        velocity -= velocity;
        body.velocity -= body.velocity;
    }
}
