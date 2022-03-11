using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{
    public PlayableDirector cutsceneDirector;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        cutsceneDirector.Play();
        Destroy(gameObject);
    }
}
