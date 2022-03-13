using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnable : MonoBehaviour
{
    public GameObject boss;
    public GameObject bossDoors;

    private void Update()
    {
        if(boss == null && bossDoors != null)
        {
            bossDoors.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if(boss != null)
            {
                boss.SetActive(true);
            }
            
            if(bossDoors != null)
            {
                bossDoors.SetActive(true);
            }
        }
    }
}
