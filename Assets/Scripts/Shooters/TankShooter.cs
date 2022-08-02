using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{
    public Transform firepointTransform;

    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    // Method to shoot a tank shell
    public override void Shoot(GameObject shellPrefab, float fireForce, float damageDone, float lifespan)
    {
        // Instantiate a TankShell as a GameObject and get it's DamageOnHit component
        GameObject newShell = Instantiate(shellPrefab, firepointTransform.position, firepointTransform.rotation) as GameObject;
        DamageOnHit doh = newShell.GetComponent<DamageOnHit>();

        // If the object has a DamageOnHit
        if (doh != null)
        {
            // Set the damageDone in the DamageOnHit to the damageDone value that was passed in
            doh.damageDone = damageDone;
            // Set the owner to the pawn that shot the shell
            doh.owner = GetComponent<Pawn>();
        }

        // Get the rigidbody component of the TankShell
        Rigidbody rb = newShell.GetComponent<Rigidbody>();
        // If it has a Rigidbody
        if (rb != null)
        {
            // Use AddForce to make it move forward
            rb.AddForce(firepointTransform.forward * fireForce);
        }

        // Destroy the TankShell when it's lifespan has expired
        Destroy(newShell, lifespan);
    }
}
