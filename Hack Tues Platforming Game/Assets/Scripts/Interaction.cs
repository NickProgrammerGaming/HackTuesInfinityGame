using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour
{

    bool inRange = false;
    public UnityEvent interactEvent;
    public bool canInteract = true;

    void Update()
    {
        if(inRange && canInteract)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                interactEvent.Invoke();
                canInteract = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            inRange = false;
        }
    }
}
