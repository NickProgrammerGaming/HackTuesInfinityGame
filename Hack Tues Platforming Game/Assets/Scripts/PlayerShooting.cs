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
    public float recoilX, recoilY;
    public Transform gunTransform;
    Vector3 startingGunPos;
    public float returnSpeed;

    private void Start()
    {
        startingGunPos = gunTransform.localPosition;
    }

    void Update()
    {
        gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, startingGunPos, returnSpeed * Time.deltaTime);
        gunTransform.localRotation = Quaternion.Lerp(gunTransform.localRotation, Quaternion.identity, returnSpeed * Time.deltaTime);

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
        float gunAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        float bulletAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        gunAngle = Mathf.Clamp(gunAngle, -90f, 90f);

        gunTransform.localRotation = Quaternion.AngleAxis(gunAngle, Vector3.forward);

        gunTransform.localPosition += gunTransform.right * recoilX;
        gunTransform.localPosition += gunTransform.up * recoilY;

        GameObject instantiatedLaser = Instantiate(laserPrefab, shootPoint.position, Quaternion.AngleAxis(bulletAngle, Vector3.forward));

        Rigidbody2D laserRb = instantiatedLaser.GetComponent<Rigidbody2D>();

        if(laserRb != null)
        {
            laserRb.AddForce(shootDirection * projectileSpeed);
        }


    }

}
