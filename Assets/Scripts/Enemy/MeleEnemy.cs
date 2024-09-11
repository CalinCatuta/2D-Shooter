using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy : MonoBehaviour, IDamageable {
    public EnemyScriptableObject enemyScript;
    private Transform player;
    private float moveSpeed;
    private float health;
    private float damage;

    private void Awake() {
        moveSpeed = enemyScript.MoveSpeed;
        damage = enemyScript.Damage;
        health = enemyScript.MaxHealth;
    }


    private void Start() {
       
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }
    //  Collisions involve a physical impact, meaning the objects will bounce off each other or otherwise react based on their physics properties.
    // Collisions is used for physical logic like Hitting.
    private void OnCollisionEnter2D(Collision2D collision) {
        // Reference the script from the collided collider and deal damage using TakeDamage().
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(damage); //Make sure to use currentDamage instead of weaponData.damage in case any damage multipliers in the future.
        }
    }
    public void TakeDamage(float dmg) {
        health -= dmg;

        if (health < 0) {
            Kill();
        }
    }
    public void Kill() { 
    Destroy(gameObject);
    }
}
