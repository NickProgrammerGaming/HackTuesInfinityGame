using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootingStaticEnemy : MonoBehaviour
{
    public new string name;
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
    public bool boss;
    public Healthbar bossHealthbar;
    public TMP_Text bossName;

    private void Start()
    {
        currentHealth = maxHealth;
        if (boss)
        {
            bossHealthbar.SetMaxHealth(maxHealth);
            bossName.text = name;
            bossHealthbar.gameObject.SetActive(true);
            bossName.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        hasSeenPlayer = Vector2.Distance(playerTransform.position, transform.position) <= seeDistance;
        if(hasSeenPlayer == true && Time.time > nextTimeToShoot)
        {
            Shoot();

            nextTimeToShoot = Time.time + 1f / fireRate;
        }

        if(currentHealth <= 0)
        {
            Die();
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
        if(boss)
        {
            bossHealthbar.SetHealth(currentHealth);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        if(boss)
        {
            bossHealthbar.gameObject.SetActive(false);
            bossName.gameObject.SetActive(false);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, seeDistance);
    }
}
