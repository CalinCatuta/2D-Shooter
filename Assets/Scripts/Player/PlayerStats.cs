using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float health = 3f;


    //I-Frames
    // Creating a sistem to stop the Player take damage infinitly every miliseconds
    // With this will make the Player take damage only after the isInvincible is false. That helps take damage after a time setted be us (example 0.5s)
    [Header("I-Frame")]
    public float invicibilityDuration;
    float invicibilityTimer;
    bool isInvincible;

    private void Update() {

        if (invicibilityTimer > 0)
        {
            invicibilityTimer -= Time.deltaTime;
        }
        // if the invincibility timer has reached 0, set the invicible flag to false
        else if (isInvincible)
        {
            isInvincible = false;
        }
    }
    public void TakeDamage(float dmg) {
        //If the player is not currently invincible, reduce health and start invincibility
        if (!isInvincible)
        {
            health -= dmg;
            Debug.Log("Damage Taken");
            invicibilityTimer = invicibilityDuration;
            // After first hit from the Enemy make the player invulnerable for next invincibiltyTimer
            isInvincible = true;
            if (health <= 0)
            {
                Kill();
            }
        }
    }
    public void Kill() {
        Debug.Log("Player Is Dead");
    }
}
