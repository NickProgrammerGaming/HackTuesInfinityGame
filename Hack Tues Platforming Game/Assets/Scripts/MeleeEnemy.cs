using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MeleeEnemy : MonoBehaviour
{

    private Rigidbody2D rb;
    public float timeBetweenAttacks;
    float nextTimeToAttack;
    public int damage;
    public float seenDistance;
    public Transform playerTransform;
    public float speed;
    public int maxHealth;
    int currentHealth;
    public float jumpForce;
    public bool boss;
    public Healthbar bossHealthbar;
    public TMP_Text bossName;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        if(boss)
        {
            bossHealthbar.SetMaxHealth(maxHealth);
        }
    }

    
    void Update()
    {
        if(Vector2.Distance(playerTransform.position, transform.position) <= seenDistance)
        {
            Vector2 moveDirection = (playerTransform.position - transform.position).normalized;
            moveDirection.y *= jumpForce;

            Vector2.MoveTowards(transform.position, playerTransform.position, speed * Time.deltaTime);
        }

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            PlayerMovement player = collision.transform.GetComponent<PlayerMovement>();
            if(player != null)
            {
                player.TakeDamage(damage);
                nextTimeToAttack = Time.time + 1f / timeBetweenAttacks;
            }
            
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
        if (boss)
        {
            bossHealthbar.gameObject.SetActive(false);
            bossName.gameObject.SetActive(false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlayerMovement player = collision.transform.GetComponent<PlayerMovement>();
        
        if (collision.transform.tag == "Player" && Time.time > nextTimeToAttack)
        {
            player.TakeDamage(damage);
            nextTimeToAttack = Time.time + 1f / timeBetweenAttacks;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, seenDistance);
    }


}
