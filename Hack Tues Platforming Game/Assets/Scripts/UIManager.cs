using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject satelliteInfoObject;
    public PlayerMovement movement;
    public PlayerShooting shooting;

    public void CloseSatelliteInfo()
    {
        satelliteInfoObject.SetActive(false);
        movement.enabled = true;
        shooting.enabled = true;
    }
}
