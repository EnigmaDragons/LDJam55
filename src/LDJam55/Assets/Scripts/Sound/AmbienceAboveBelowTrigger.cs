using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AmbienceAboveBelowTrigger : MonoBehaviour
{

    public EventReference ambienceEvent;
    public EventReference musicEvent;
    EventInstance ambienceInstance;

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag(other.tag))
        {
            ambienceInstance = RuntimeManager.CreateInstance(ambienceEvent);
            ambienceInstance.start();
        }
    }

    private void OnDisable()
    {
        ambienceInstance.release();
    }
}
