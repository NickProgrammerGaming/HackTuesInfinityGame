using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    public BulletMode bulletMode;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(bulletMode == BulletMode.Player)
        {
            if (collision.transform.tag == "Enemy")
            {
                PassiveEnemy pEnemy = collision.gameObject.GetComponent<PassiveEnemy>();
                ShootingStaticEnemy sEnemy = collision.gameObject.GetComponent<ShootingStaticEnemy>();
                Boss boss = collision.gameObject.GetComponent<Boss>();

                if (pEnemy != null)
                {
                    pEnemy.TakeDamage(damage);
                }
                else if (sEnemy != null)
                {
                    sEnemy.TakeDamage(damage);
                }
                else
                {
                    boss.TakeDamage(damage);
                }

            }
        }
        else
        {
            if (collision.transform.tag == "Player")
            {
                PlayerMovement player = collision.transform.GetComponent<PlayerMovement>();
                player.TakeDamage(damage);

            }
        }

        Destroy(gameObject);
    }


    public enum BulletMode { Player, Enemy }
}
