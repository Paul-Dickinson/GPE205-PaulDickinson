using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{

    // Start is called before the first frame update
    public abstract void Start();

    // Move recieves a Vector3 and float to determine how the object should move
    public abstract void Move(Vector3 direction, float speed);

    // Rotate recieves three floats and uses them to effect the pitch, yaw, and roll of the object
    public abstract void Rotate(float turnSpeed);

}
