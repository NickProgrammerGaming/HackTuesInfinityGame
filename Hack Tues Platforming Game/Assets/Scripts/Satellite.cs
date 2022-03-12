using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Satellite : MonoBehaviour
{
    public List<string> info;
    public List<Sprite> images;

    public GameObject infoUI;
    public TMP_Text satelliteText;
    public Image satelliteImage;

    public PlayerMovement movement;
    public PlayerShooting shooting;

    public void Interact()
    {
        int selected_object = Random.Range(0, info.Count);

        infoUI.SetActive(true);
        satelliteText.text = info[selected_object];
        satelliteImage.sprite = images[selected_object];

        movement.enabled = false;
        shooting.enabled = false;
    }
}
