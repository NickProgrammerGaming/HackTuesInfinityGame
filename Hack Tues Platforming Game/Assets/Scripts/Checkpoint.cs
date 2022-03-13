using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
            player.Respawnx = transform.position.x;
            player.Respawny = transform.position.y;
            Destroy(gameObject);

        }

        
    }
}
