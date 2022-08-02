using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // A static GameManager varable to store the one GameManager instance we want
    public static GameManager instance;

    // List to hold all of our players, controllers, and Pawns
    public List<PlayerController> players;
    public List<Controller> controllers;
    public List<Pawn> pawns;

    private void Awake()
    {
        // Checks if there are any other GameManager objects by checking if instance is empty
        if (instance == null)
        {
            // If instance is empty, then there are no other GameManager objects
            // Delare that this instance is the instance
            instance = this;

            // Tells unity to not destroy this instance when new scenes are loaded
            DontDestroyOnLoad(gameObject);
        }
        else {
            //If instance isn't empty, another GameManager object exists; so destroy this one
            Destroy(gameObject);
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
