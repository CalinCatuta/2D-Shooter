using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSript : MonoBehaviour
{
    private float damage = 1f;

    //This logic is implemented with the help of Interface.
    //The Object will Collide with another Object and if that Object have the IDamageable
    //then the Code will know witch Object it is and he will call the TakeDamage inside that Object.
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.TryGetComponent(out IDamageable target))
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
