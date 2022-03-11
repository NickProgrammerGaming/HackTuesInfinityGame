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
    public Transform GroundCheckCollider;

    private bool isGrounded;
    public Transform groundCheckPos;
    public float groundCheckRadius;
    public LayerMask GroundLayer;

    public int maxHealth;
    int currentHealth;
    /*
    private bool isVoid = false;
    public LayerMask Void;*/
    // Start is called before the first frame update
    void Start()
    {
        rigbod = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }
    /*void Restart()
    {
        isVoid = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheckCollider.position, GroundCheckRadius, Void);
        if (colliders.Length > 0)
            isVoid = true;
        Vector3 Coor = transform.localPosition;
        if (isVoid)
        {
            Coor.y = 6;
            Coor.x = -8;
            rigbod.velocity = new Vector2(rigbod.velocity.x, 0);
        }
        transform.localPosition = Coor;
    }
    */


    // Update is called once per frame
    void Update()
    {
        // Restart();
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
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void ChangeScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}