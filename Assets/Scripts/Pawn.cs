using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class set to abstract as it will be overridden
public abstract class Pawn : MonoBehaviour
{
    // Variables for turn speed and movement speed
    public float turnSpeed;
    public float moveSpeed;

    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    // Functions for moving forwards, moving backwards, turning clockwise, and turning counter-clockwise; respectively
    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void TurnClockwise();
    public abstract void TurnCounterClockwise();
}
