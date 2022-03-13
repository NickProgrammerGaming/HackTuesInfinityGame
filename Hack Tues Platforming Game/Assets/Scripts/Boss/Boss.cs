using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Boss : MonoBehaviour
{
    public new string name;
    public int maxHealth;
    int currentHealth;
    public Healthbar bossHealthbar;
	public Transform player;
	public bool isFlipped = false;
	public float projectileSpeed;
	public GameObject bulletPrefab;
	public int groundSlamBullets;
	public float burstFireRate;
	float nextTimeToShoot;
	public TMP_Text bossName;



	void Start()
    {
		currentHealth = maxHealth;
		
		bossHealthbar.SetMaxHealth(maxHealth);
		bossName.text = name;
		bossHealthbar.gameObject.SetActive(true);
		bossName.gameObject.SetActive(true);

	}


    void Update()
    {
		if (currentHealth <= 0)
		{
			Die();
		}
	}
	
	public void LookAtPlayer()
	{
		Vector3 flipped = transform.localScale;
		flipped.z *= -1f;

		if (transform.position.x > player.position.x && isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = false;
		}
		else if (transform.position.x < player.position.x && !isFlipped)
		{
			transform.localScale = flipped;
			transform.Rotate(0f, 180f, 0f);
			isFlipped = true;
		}
	}

	public void GroundSlam()
    {
		for(int i = 0; i < groundSlamBullets; i++)
        {
			if(nextTimeToShoot < Time.time)
            {
				Shoot();
				nextTimeToShoot = Time.time + 1f / burstFireRate;
            }
		}
		
	}

	public void Shoot()
	{
		Vector2 shootDirection = (player.position - transform.position).normalized;
		float bulletAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

		GameObject instantiatedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.AngleAxis(bulletAngle, Vector3.forward));

		Rigidbody2D bulletRb = instantiatedBullet.GetComponent<Rigidbody2D>();

		if (bulletRb != null)
		{
			bulletRb.AddForce(shootDirection * projectileSpeed);
		}
	}

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		bossHealthbar.SetHealth(currentHealth);

	}

	public void Die()
    {
		Destroy(gameObject);
		bossHealthbar.gameObject.SetActive(false);
		bossName.gameObject.SetActive(false);
	}

}


   
