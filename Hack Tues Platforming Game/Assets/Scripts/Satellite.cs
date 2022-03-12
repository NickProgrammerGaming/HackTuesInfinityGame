using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Satellite : MonoBehaviour
{
    public string info;
    public Sprite images;

    public GameObject infoUI;
    public TMP_Text satelliteText;
    public Image satelliteImage;

    public PlayerMovement movement;
    public PlayerShooting shooting;

    

    public void Interact()
    {
        

        infoUI.SetActive(true);
        satelliteText.text = info;
        satelliteImage.sprite = images;

        movement.enabled = false;
        shooting.enabled = false;

        
    }
}
