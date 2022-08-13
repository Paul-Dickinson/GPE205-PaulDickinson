using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    public float damageDone;
    public Pawn owner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        // Get the health component from the gameObject who's collider that we collided with
        HealthComponent otherHealthComponent = other.gameObject.GetComponent<HealthComponent>();

        // If it has a health component, then deal damage
        if(otherHealthComponent != null)
        {
            otherHealthComponent.TakeDamage(damageDone, owner);
        }

        Destroy(gameObject);
    }
}
