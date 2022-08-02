using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Set health to max
        currentHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to take damage when hit by a projectile
    public void TakeDamage(float damageTaken, Pawn source)
    {
        currentHealth = currentHealth - damageTaken;
        Debug.Log(source.name + " did " + damageTaken + " damage to " + gameObject.name);
        Debug.Log("I have " + currentHealth + " Remaining.");
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        

        // Checks if currentHealth is equal to or less than zero
        if (currentHealth <= 0)
        {
            // If it is, kill the object
            Die(source);
        }
    }

    public void Heal(float healingRecieved, Pawn source)
    {
        currentHealth = currentHealth + healingRecieved;
        Debug.Log(source.name + "healed" + gameObject.name + "for" + healingRecieved);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    // Method to destroy the gameObject when killed
    public void Die(Pawn source)
    {
        Destroy(gameObject);
    }
}
