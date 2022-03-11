using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float fireRate;
    float nextTimeToShoot;
    public float projectileSpeed;
    public GameObject laserPrefab;
    public Transform shootPoint;

    void Start()
    {
        
    }

   
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextTimeToShoot)
        {
            Shoot();

            nextTimeToShoot = Time.time + 1f / fireRate;
        }
    }

    void Shoot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector3(mousePosition.x, mousePosition.y, 0f);
        Vector3 shootDirection = (mousePosition - transform.position).normalized;
        float bulletAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        GameObject instantiatedLaser = Instantiate(laserPrefab, shootPoint.position, Quaternion.AngleAxis(bulletAngle, Vector3.forward));

        Rigidbody2D laserRb = instantiatedLaser.GetComponent<Rigidbody2D>();

        if(laserRb != null)
        {
            laserRb.AddForce(shootDirection * projectileSpeed);
        }


    }
}
