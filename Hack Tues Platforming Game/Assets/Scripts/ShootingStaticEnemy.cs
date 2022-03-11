using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStaticEnemy : MonoBehaviour
{
    public float fireRate;
    float nextTimeToShoot;
    public GameObject bulletPrefab;
    public Transform playerTransform;
    public float projectileSpeed;
    bool hasSeenPlayer;
    public float seeDistance;
    public int damage;
    public int maxHealth;
    int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        hasSeenPlayer = Vector2.Distance(playerTransform.position, transform.position) <= seeDistance;
        if(hasSeenPlayer == true && Time.time > nextTimeToShoot)
        {
            Shoot();

            nextTimeToShoot = Time.time + 1f / fireRate;
        }
        
    }

    void Shoot()
    {
        Vector2 shootDirection = (playerTransform.position - transform.position).normalized;
        float bulletAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        GameObject instantiatedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(bulletAngle, Vector3.forward));

        Rigidbody2D bulletRb = instantiatedBullet.GetComponent<Rigidbody2D>();

        if (bulletRb != null)
        {
            bulletRb.AddForce(shootDirection * projectileSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerMovement player = collision.transform.GetComponent<PlayerMovement>();
            player.TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, seeDistance);
    }
}
