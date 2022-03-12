using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveEnemy : MonoBehaviour
{
    public int damage;
    public int maxHealth;
    int currentHealth;
    public float speed;
    public Transform groundDetect;
    public LayerMask ground;
    public float groundDistance;
    public float wallDistance;
    private Rigidbody2D rb;
    public Collider2D bodyCollider;
    bool patrol;
    bool turn;
    public float damageCooldown;
    float nextTimeToAttack;


    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        patrol = true;
    }

    private void FixedUpdate()
    {
        turn = !Physics2D.OverlapCircle(groundDetect.position, groundDistance, ground);
    }

    private void Update()
    {
        if(turn || bodyCollider.IsTouchingLayers(ground))
        {
            Flip();
        }

        if(patrol)
        {
            rb.velocity = new Vector3(speed * Time.fixedDeltaTime, rb.velocity.y);
        }
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
    void Flip()
    {
        patrol = false;
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        speed = -speed;
        patrol = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
            player.TakeDamage(damage);
            nextTimeToAttack = Time.time + 1f / damageCooldown;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && nextTimeToAttack < Time.time)
        {
            PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
            player.TakeDamage(damage);
            nextTimeToAttack = Time.time + 1f / damageCooldown;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(groundDetect.position, groundDistance);
    }


}
