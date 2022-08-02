using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    private bool isTouchingGround;

    private float shotTimer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        shotTimer = 0;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        shotTimer = shotTimer - Time.deltaTime;
    }

    public void OnCollisionEnter()
    {
        isTouchingGround = true;
    }

    public void OnCollisionExit()
    {
        isTouchingGround = false;
    }

    // Functions for moving forwards, moving backwards, turning clockwise, and turning counter-clockwise; respectively
    public override void MoveForward()
    {
        if(isTouchingGround == true)
        {
            mover.Move(transform.forward, moveSpeed);
        }
    }
    public override void MoveBackward()
    {
        if(isTouchingGround == true)
        {
            mover.Move(transform.forward, -moveSpeed);
        }
    }
    public override void TurnClockwise()
    {
        if(isTouchingGround == true)
        {
            mover.Rotate(turnSpeed);
        }
    }
    public override void TurnCounterClockwise()
    {
        if(isTouchingGround == true)
        {
            mover.Rotate(-turnSpeed);
        }
    }

    public override void Shoot()
    {
        if (shotTimer <= 0)
        {
            shooter.Shoot(shellPrefab , fireForce, damageDone, shellLifespan);
            shotTimer = secondsPerShot;
        }
        
    }
}
