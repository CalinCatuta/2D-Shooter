using UnityEngine;

public class EnemyBulletScript : MonoBehaviour {
    [HideInInspector]
    public float speed = 14f;
    [HideInInspector]
    public float lifeTime = 2f;
    [HideInInspector]
    public float currentDamage = 2f;

    private Vector3 direction;

    public void SetTarget(Vector3 target) {
        // Calculate the direction to move the projectile in
        direction = (target - transform.position).normalized;
        Destroy(gameObject, lifeTime);  // Destroy the projectile after its lifetime
    }

    void Update() {
        // Move in the calculated direction
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // Reference the script from the collided collider and deal damage using TakeDamage().
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit");
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage); //Make sure to use currentDamage instead of weaponData.damage in case any damage multipliers in the future.
            Destroy(gameObject);
        }
    }
}
