using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    // KeyCode variables to hold keys used to detect movement
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode turnClockwiseKey;
    public KeyCode turnCounterClockwiseKey;

    // Start is called before the first frame update
    public override void Start()
    {
        // Runs the Start() function from the parent class
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Runs the ProcessInputs function
        ProcessInputs();

        // Runs the Update() function from
        base.Update();
    }

    // Checks if any KeyCode varible is being pressed
    public void ProcessInputs()
    {
        if (Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
        }
        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
        }
        if (Input.GetKey(turnClockwiseKey))
        {
            pawn.TurnClockwise();
        }
        if (Input.GetKey(turnCounterClockwiseKey))
        {
            pawn.TurnCounterClockwise();
        }
    }
}
