using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class set to abstract as it will be overridden
public abstract class Pawn : MonoBehaviour
{
    // Variables for turn speed and movement speed
    public float turnSpeed;
    public float moveSpeed;

    // Variable to hold the Mover component
    public Mover mover;
    // Variable to hold the Shooter component
    public Shooter shooter;

    // Variables for shooting a projectile
    public GameObject shellPrefab;
    public float fireForce;
    public float damageDone;
    public float shellLifespan;

    // Variables for fire rate
    public float fireRate;
    public float secondsPerShot;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();
        shooter = GetComponent<Shooter>();

        // Check if there is a GameManager object
        if (GameManager.instance != null)
        {
            // If there is, check if it has a list to track pawns
            if (GameManager.instance.pawns != null)
            {
                // If there is, add this Pawn to it
                GameManager.instance.pawns.Add(this);
            }
        }

        secondsPerShot = 1 / fireRate;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public virtual void OnDestroy()
    {
        // Check if there is a GameManager object
        if (GameManager.instance != null)
        {
            // If there is, check if it has a list to track pawns
            if (GameManager.instance.pawns != null)
            {
                // If there is, remove this Pawn to it
                GameManager.instance.pawns.Remove(this);
            }
        }
    }

    // Methods for moving forwards, moving backwards, turning clockwise, and turning counter-clockwise; respectively
    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void TurnClockwise();
    public abstract void TurnCounterClockwise();

    // Method for shooting a projectile
    public abstract void Shoot();
}
