using UnityEngine;
using System.Collections;

public class RangeEnemy : MonoBehaviour, IDamageable {
    public float moveSpeed = 3f;               // Speed at which the enemy moves.
    public float stopDistance = 5f;            // Ideal distance from the player to stop and shoot.
    public float retreatDistance = 3f;         // Distance at which the enemy will retreat if the player is too close.
    public float fireRate = 2f;                // Cooldown between shots (in seconds).

    private float nextTimeToShoot = 0f;        // Timestamp to keep track of the shooting cooldown.
    private Transform player;                  // Reference to the player’s transform.
    private Rigidbody2D rb;                    // Reference to the Rigidbody2D component for movement.
    private Transform weapon;                  // Reference to the weapon (child object).
    private Transform bulletSpawnPoint;        // Reference to the bullet spawn point.
    private Vector2 movement;                  // Store calculated movement.

    private enum EnemyState { Moving, Shooting, Idle }    // State enumeration.
    private EnemyState currentState;                      // Current state of the enemy.

    private void Start() {
        // Get references to necessary components.
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        weapon = transform.Find("EnemyWeapon");           // Assuming the child is named "EnemyWeapon".
        bulletSpawnPoint = weapon.Find("EnemyBulletSpawnPoint"); // Assuming the bullet spawn point is a child.

        currentState = EnemyState.Moving;                 // Start by moving towards the player.
    }

    private void Update() {
        // Always face the player
        FacePlayer();

        // Update state machine logic.
        switch (currentState)
        {
            case EnemyState.Moving:
                MoveTowardsPlayer();
                break;
            case EnemyState.Shooting:
                ShootAtPlayer();
                break;
            case EnemyState.Idle:
                // Optionally, the enemy could wait or idle after shooting.
                break;
        }
    }

    private void FacePlayer() {
        Vector2 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
    }

    private void MoveTowardsPlayer() {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance)
        {
            // Move closer to the player if too far
            Vector2 direction = (player.position - transform.position).normalized;
            movement = direction * moveSpeed;
            rb.MovePosition((Vector2)transform.position + movement * Time.deltaTime);
        }
        else if (distanceToPlayer < retreatDistance)
        {
            // Move away from the player if too close
            Vector2 direction = (transform.position - player.position).normalized;
            movement = direction * moveSpeed;
            rb.MovePosition((Vector2)transform.position + movement * Time.deltaTime);
        }
        else
        {
            // Enter shooting state if within range
            currentState = EnemyState.Shooting;
        }
    }

    private void ShootAtPlayer() {
        // If the cooldown has passed, shoot towards the player
        if (Time.time >= nextTimeToShoot)
        {
            // Perform shooting logic
            Shoot();

            // Set cooldown for the next shot
            nextTimeToShoot = Time.time + fireRate;
        }

        // Check if player has moved out of range and change state back to moving
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > stopDistance || distanceToPlayer < retreatDistance)
        {
            currentState = EnemyState.Moving;
        }
    }

    private void Shoot() {
        // Create a bullet and fire towards the player
        // Assuming Bullet is another prefab with a script handling its own movement
        GameObject bullet = Instantiate(Resources.Load("BulletPrefab"), bulletSpawnPoint.position, bulletSpawnPoint.rotation) as GameObject;
        bullet.GetComponent<EnemyBulletScript>().SetTarget(player.position);
    }

    //public void TakeDamage(float dmg) {
    //    health -= dmg;
    //    Debug.Log("Hit");
    //    if (health < 0)
    //    {
    //        Kill();
    //    }
    //}
    //public void Kill() {
    //    Destroy(gameObject);
    //}
}
