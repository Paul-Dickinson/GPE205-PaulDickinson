using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    // Start is called before the first frame update
    public override void Start()
    {
        // Run the parent (base) Start
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        // Calling the method to make decisions
        MakeDecisions();
        // Run the parent (base) Update
        base.Update();
    }

    public void MakeDecisions()
    {
        Debug.Log("Making Decisions");
    }
}
