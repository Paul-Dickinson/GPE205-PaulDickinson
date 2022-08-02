using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Controller : MonoBehaviour
{
    // Variable to hold the pawn that will be controlled
    public Pawn pawn;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // Check if there is a GameManager object
        if (GameManager.instance != null)
        {
            // If there is, check if it has a list to track controllers
            if (GameManager.instance.controllers != null)
            {
                // If there is, add this Controller to it
                GameManager.instance.controllers.Add(this);
            }
        }
        
    }

    public virtual void OnDestroy()
    {
        // Check if there is a GameManager object
        if (GameManager.instance != null)
        {
            // If there is, check if it has a list to track controllers
            if (GameManager.instance.controllers != null)
            {
                // If there is, remove this Controller to it
                GameManager.instance.controllers.Remove(this);
            }
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
