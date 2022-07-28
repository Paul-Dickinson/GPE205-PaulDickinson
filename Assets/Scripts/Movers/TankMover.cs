using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMover : Mover
{
    // RigidBody variable to hold the gameObject's RigidBody component
    public Rigidbody rigidBody;

    // Start is called before the first frame update
    public override void Start()
    {
        // Stores the RigidBody component in the created variable
        rigidBody = GetComponent<Rigidbody>();
    }

    public override void Move(Vector3 direction, float speed)
    {
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        rigidBody.MovePosition(rigidBody.position + moveVector);

    }

    public override void Rotate(float turnSpeed)
    {
        transform.Rotate(0, turnSpeed * Time.deltaTime, 0);
    }
}
