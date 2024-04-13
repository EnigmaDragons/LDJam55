using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AmbienceAboveBelowTrigger : MonoBehaviour
{

    public EventReference ambienceEvent;
    public EventReference musicUnderWaterEvent;
    EventInstance ambienceInstance;
    EventInstance musicInstance;

    private void Start()
    {
        ambienceInstance = RuntimeManager.CreateInstance(ambienceEvent);
        ambienceInstance.start();

        musicInstance = RuntimeManager.CreateInstance(musicUnderWaterEvent);
        musicInstance.start();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("player"); 
            musicInstance = RuntimeManager.CreateInstance(musicUnderWaterEvent);
            musicInstance.start();
        }
    }

    private void OnDisable()
    {
        ambienceInstance.release();
        musicInstance.release();
    }
}
