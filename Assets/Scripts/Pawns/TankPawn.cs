using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    private float shotTimer;

    private Transform lastPosition;
    private float shotAudible;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        shotTimer = 0;
        lastPosition = transform;
        shotAudible = 0;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        SetSound();
        shotTimer = shotTimer - Time.deltaTime;
    }

    // Functions for moving forwards, moving backwards, turning clockwise, and turning counter-clockwise; respectively
    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed);
    }
    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed);
        
    }
    public override void TurnClockwise()
    {
        mover.Rotate(turnSpeed);
    }
    public override void TurnCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }

    public override void Shoot()
    {
        if (shotTimer <= 0)
        {
            shooter.Shoot(shellPrefab , fireForce, damageDone, shellLifespan);
            shotTimer = secondsPerShot;
            shotAudible = shotAudibleTime;
        }
        
    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        // Find the vector to the target
        Vector3 vectorToTarget = targetPosition - transform.position;
        // Find the rotation required to aim to that vector
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        // Rotate to that vector as much as our turn speed allows
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public void SetSound()
    {
        shotAudible = shotAudible - Time.deltaTime;

        // If a shot from this object is still audible
        if(shotAudible <= 0)
        {
            //Then make noise equal to the distance that a shot can be heard
            noiseMaker.volumeDistance = shotAudibleDistance;
        }
        // If a shot isn't audible, but the pawn has moved since last frame
        else if (lastPosition != transform)
        {
            // Then make noise equal to the distance that moving can be heard
            noiseMaker.volumeDistance = moveAudibleDistance;
        }
        // If a shot is not audible, nor is the pawn moving
        else
        {
            // Then make noise equal to the distance that idling can be heard
            noiseMaker.volumeDistance = idleAudibleDistance;
        }

        lastPosition = transform;
    }
}
