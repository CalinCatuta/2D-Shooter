using UnityEngine;

public class EnemyBulletScript : MonoBehaviour {
    public float speed = 5f;
    private float damage = 2f;
    private Vector2 target;

    public void SetTarget(Vector2 targetPosition) {
        target = targetPosition;
        Destroy(gameObject, 2f); // Destroy bullet after 5 seconds if it doesn't hit anything.
    }

    private void Update() {
        // Move bullet towards the target
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Optionally, add collision detection with player or environment here
    }

    //  Collisions involve a physical impact, meaning the objects will bounce off each other or otherwise react based on their physics properties.
    // Collisions is used for physical logic like Hitting.
    private void OnCollisionEnter2D(Collision2D collision) {
        // Reference the script from the collided collider and deal damage using TakeDamage().
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(damage); //Make sure to use currentDamage instead of weaponData.damage in case any damage multipliers in the future.
            Destroy(gameObject);
        }
    }
}
