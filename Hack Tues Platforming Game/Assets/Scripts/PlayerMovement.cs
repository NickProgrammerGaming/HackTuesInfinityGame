using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float MovementSpeed;
    public float JumpForce;

    private Rigidbody2D rigbod;
    private float move;
    //GroundCheck
    private bool isGrounded;
    public Transform groundCheckPos;
    public float groundCheckRadius;
    public LayerMask GroundLayer;

    public int maxHealth;
    int currentHealth;
    
    private bool isVoid = false;
    public LayerMask Void;
    
    public float Respawnx;
    public float Respawny;

    public LayerMask checkpointMask;

    public Healthbar playerHealthbar;
    public GameObject gameOverScreen;
    public float restartDelay;



    // Start is called before the first frame update
    void Start()
    {
        rigbod = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        playerHealthbar.SetMaxHealth(maxHealth);
    }
    

    // Update is called once per frame
    void Update()
    {
        //Physics2D.IgnoreLayerCollision(7, 11);
        move = Input.GetAxis("Horizontal");
        animator.SetFloat("Running", Mathf.Abs(move));
        Vector3 chScale = transform.localScale;
        if (move < 0)
        {
            chScale.x = -1;
        }
        if (move > 0)
        {
            chScale.x = 1;
        }

        transform.localScale = chScale;
        rigbod.velocity = new Vector2(move * MovementSpeed, rigbod.velocity.y);
        isGrounded = Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, GroundLayer);
        animator.SetBool("IsGrounded", isGrounded);
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            rigbod.velocity = new Vector2(rigbod.velocity.x, JumpForce);

        }
        if (Input.GetButtonUp("Jump") && rigbod.velocity.y > 0)
        {
            rigbod.velocity = new Vector2(rigbod.velocity.x, rigbod.velocity.y * .5f);  
        }
        

        if (rigbod.velocity.y < -0.01f)
        {
            animator.SetBool("Jump", false);

        }
        else if (rigbod.velocity.y > 0.1f)
        {
            animator.SetBool("Jump", true);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos.position, groundCheckRadius, Void);
        if (colliders.Length > 0)
        {
            isVoid = true;
            TakeDamage(1);
            Die();
        }

        if(currentHealth <= 0)
        {
            gameOverScreen.SetActive(true);
        }
            

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Die()
    {
        Vector3 Coor = transform.localPosition;
        if (isVoid)
        {
            Coor.y = Respawny;
            Coor.x = Respawnx;
            rigbod.velocity = new Vector2(rigbod.velocity.x, 0);
        }
        transform.localPosition = Coor;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        playerHealthbar.SetHealth(currentHealth);

    }

    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}