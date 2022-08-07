using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    // Variables to hold the different states and which state the AI is currently in
    public enum AIState { Idle, Chase, Attack, Patrol, ChooseTarget};
    public AIState currentState;
    // Variable to hold when the state is changed
    private float lastStateChangeTime;

    // Variable to hold the AI's target
    public GameObject target;

    // Variable to hold information for Chase state
    public float chaseDistance;

    // Variable to hold information for the Flee state
    public float fleeDistance;
    public float fleeDistancePercentModifier;

    // Variables to hold information for the Patrol state
    public Transform[] waypoints;
    public float waypointStopDistance;
    private int currentWaypoint = 0;
    public bool isLooping;

    // Variable to hold information for perception
    public float hearingDistance;
    public float fieldOfView;
    public float sightDistance;
    private float perceptionTimer;
    public List<Pawn> objectsHeard;
    public List<Pawn> objectsSeen;


    // Start is called before the first frame update
    public override void Start()
    {
        // Run the parent (base) Start
        base.Start();

        ChangeState(AIState.Idle);
        perceptionTimer = 0;
    }

    // Update is called once per frame
    public override void Update()
    {
        perceptionTimer = perceptionTimer - Time.deltaTime;
        if (perceptionTimer <= 0)
        {
            Percieve();
            perceptionTimer = 1/5;
        } 

        
        // Calling the method to make decisions
        MakeDecisions();
        // Run the parent (base) Update
        base.Update();
    }

    public void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.Idle:
                // Do idle work
                DoIdleState();
                // Check for transitions
                if (IsDistanceLessThan(target, chaseDistance))
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Chase:
                DoChaseState();
                break;
            case AIState.Attack:
                DoAttackState();
                break;
            case AIState.ChooseTarget:
                DoChangeTargetState();
                break;

            
        }
    }
    public void Percieve()
    {
        foreach (Pawn pawn in GameManager.instance.pawns)
        {
            // Boolean to hold if
            bool canHear = CanHear(pawn.transform.root.gameObject);
            if (canHear == true && objectsHeard.Contains(pawn) == false)
            {
                objectsHeard.Add(pawn);
            }
            else if (canHear == false && objectsHeard.Contains(pawn) == true)
            {
                objectsHeard.Remove(pawn);
            }

            bool canSee = CanSee(pawn.transform.root.gameObject);
            if (canSee == true && objectsSeen.Contains(pawn) == false)
            {
                objectsSeen.Add(pawn);
            }
            else if (canSee == false && objectsSeen.Contains(pawn) == true)
            {
                objectsSeen.Remove(pawn);
            }

        }
    }

    // Method to change the state of the AI
    public virtual void ChangeState ( AIState newState)
    {
        // Change the current state
        currentState = newState;
        // Save the time the state is changed
        lastStateChangeTime = Time.time;
    }

    // Methods for each state of the AI
    protected virtual void DoIdleState()
    {
        // Do nothing
    }
    protected virtual void DoChaseState()
    {
        // Seek our target
        Seek(target);
    }
    protected virtual void DoAttackState()
    {
        // Chase the target
        Seek(target);
        // Shoot
        Shoot();
    }
    protected virtual void DoFleeState()
    {
        // Flee from the target
        Flee();
    }
    protected virtual void DoPatrolState()
    {
        // Patrol around the map
        Patrol();
    }
    protected virtual void DoChangeTargetState()
    {

    }


    // Methods for each action of the AI
    public void Seek(GameObject target)
    {
        // Rotate towards the target
        pawn.RotateTowards(target.transform.position);
        // Move forward
        pawn.MoveForward();
    }
    public void Seek(Vector3 targetPosition)
    {
        // Rotate towards the position
        pawn.RotateTowards(targetPosition);
        // Move forward
        pawn.MoveForward();
    }
    public void Seek(Transform targetTransform)
    {
        // Seek the position of the target transform
        Seek(targetTransform.position);
    }
    public void Seek(Pawn targetPawn)
    {
        // Seek the transform of the target pawn
        Seek(targetPawn.transform);
    }
    public void Shoot()
    {
        //Tell the pawn to shoot
        pawn.Shoot();
    }
    public void Flee()
    {
        // Find the vector that points to the target
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        // Find the opposite of the previous vector
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        // Find the distance from the target
        float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position);
        // Find the percentage of our flee distance we want to seek
        float percentOfFleeDistance = targetDistance / fleeDistance;
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);
        float fleeDistanceSeekModifier = 1 - percentOfFleeDistance + (fleeDistancePercentModifier);
        // Find the vector we want to go towards to flee
        Vector3 fleeVector = vectorAwayFromTarget.normalized * fleeDistance * fleeDistanceSeekModifier;
        // Seek the vector we want to go down to flee
        Seek(pawn.transform.position + fleeVector);
        
    }
    public void Patrol()
    {
        // If we have enough waypoints in our list to move to a current waypoint
        if (waypoints.Length > currentWaypoint)
        {
            // Then seek that waypoint
            Seek(waypoints[currentWaypoint]);
            //If we are close enough to the current waypoint, increment to the next waypoint
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) < waypointStopDistance)
            {
                currentWaypoint++;
            }
            else
            {
                //If the patrol is looping
                if (isLooping == true)
                {
                    //Then restart the patrol
                    RestartPatrol();
                }
            }
        }
    }
    public void RestartPatrol()
    {
        // Set the index to 0
        currentWaypoint = 0;
    }


    // Methods for each conditional of the AI
    protected bool IsDistanceLessThan (GameObject target, float distance)
    {
        if (Vector3.Distance (pawn.transform.position, target.transform.position) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected bool IsHasTarget()
    {
        // Return true if we have a target, false if we don't
        return (target != null);
    }
    public bool CanHear(GameObject target)
    {
        // Get the target's NoiseMaker
        NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
        // If it doesn't have one, it can't be heard
        if (noiseMaker == null)
        {
            return false;
        }
        // If it is making 0 noise, then it can't be heard
        if (noiseMaker.volumeDistance <= 0)
        {
            return false;
        }

        // If it is making noise, add the volumeDistance from the noisemaker to the hearingDistance of this AI
        float totalDistance = noiseMaker.volumeDistance + hearingDistance;
        // If the distance between us and the pawn is lower than the totalDistance
        if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
        {
            // Then we can hear the target
            return true;
        }
        else
        {
            // If it isn't, then we can't hear it
            return false;
        }
    }
    public bool CanSee(GameObject target)
    {
        // Find the vector from the agent and the target
        Vector3 agentToTargetVector = target.transform.position - transform.position;
        // Find the angle between the direction our agent is facing and the vector to the target
        float angleToTarget = Vector3.Angle(agentToTargetVector, pawn.transform.forward);
        // If that angle is less than our field of view
        if (angleToTarget < fieldOfView)
        {
            // Then cast a ray at the target
            RaycastHit2D lineOfSight = Physics2D.Raycast(transform.position, agentToTargetVector, sightDistance);
            // If the ray hit the target
            if (lineOfSight.transform == target.transform)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
