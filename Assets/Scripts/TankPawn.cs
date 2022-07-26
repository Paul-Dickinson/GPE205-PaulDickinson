using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    // Functions for moving forwards, moving backwards, turning clockwise, and turning counter-clockwise; respectively
    public override void MoveForward()
    {
        Debug.Log("Move Forwards");
    }
    public override void MoveBackward()
    {
        Debug.Log("Move Backwards");
    }
    public override void TurnClockwise()
    {
        Debug.Log("Turn Clockwise");
    }
    public override void TurnCounterClockwise()
    {
        Debug.Log("Turn Counter Clockwise");
    }
}
