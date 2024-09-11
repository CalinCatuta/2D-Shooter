using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {
    public GameObject bulletPrefab;    // Reference to the bullet prefab
    public Transform firePoint;        // The point where bullets will be fired from (e.g., front of the weapon)
    public float bulletSpeed = 20f;    // Speed at which bullets move
    public float fireRate = 2f;        // 1 bullet per second
    private float nextTimeToFire = 0f; // Tracks the cooldown between shots

    public Camera cam;                 // Reference to the camera (to get the mouse position)
    private Vector2 mousePos;          // Store mouse position in world space

    void Update() {
        // Get mouse position in world coordinates
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        // Check if the left mouse button is pressed and cooldown has passed
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            // Update next allowed fire time
            nextTimeToFire = Time.time + 1f / fireRate;

            // Call shooting function
            Shoot();
        }
    }

    void Shoot() {
        // Instantiate the bullet at the fire point position
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Destroy bullet after 2f
        Destroy(bullet, 2f);

        // Calculate the direction between fire point and mouse position
        Vector2 shootingDirection = (mousePos - (Vector2)firePoint.position).normalized;

        // Set bullet's velocity in the direction of the mouse position
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shootingDirection * bulletSpeed;
    }

}
