using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSript : MonoBehaviour
{
    private float damage = 1f;
    private void OnCollisionEnter2D(Collision2D collision) {
        // Reference the script from the collided collider and deal damage using TakeDamage().
        if (collision.gameObject.CompareTag("Enemy"))
        {
            MeleEnemy enemy = collision.gameObject.GetComponent<MeleEnemy>();
            enemy.TakeDamage(damage); //Make sure to use currentDamage instead of weaponData.damage in case any damage multipliers in the future.
            Destroy(gameObject);
        }
    }
}
