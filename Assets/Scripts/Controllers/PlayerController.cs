using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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
        // Check if there is a GameManager object
        if (GameManager.instance != null)
        {
            // If there is, check if it has a list to track players
            if (GameManager.instance.players != null)
            {
                // If there is, add this PlayerController to it
                GameManager.instance.players.Add(this);
            }
        }

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

    public void OnDestroy()
    {
        // Check if there is a GameManager object
        if (GameManager.instance != null)
        {
            // If there is, check if it has a list to track players
            if (GameManager.instance.players != null)
            {
                // If there is, remove this PlayerController to it
                GameManager.instance.players.Remove(this);
            }
        }
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
