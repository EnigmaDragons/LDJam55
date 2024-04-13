using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AmbienceAboveBelowTrigger : MonoBehaviour
{

    public EventReference ambienceRef;
    public EventReference musicUnderWaterRef;
    public EventReference musicTempleRef;
    EventInstance ambienceInstance;
    EventInstance musicUnderWaterInstance;
    EventInstance musicTempleInstance; 

    private void Start()
    {
        ambienceInstance = RuntimeManager.CreateInstance(ambienceRef);
        ambienceInstance.start();

        musicTempleInstance = RuntimeManager.CreateInstance(musicTempleRef);
        musicTempleInstance.start();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("player one"); 
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player");
            musicUnderWaterInstance = RuntimeManager.CreateInstance(musicUnderWaterRef);
            musicUnderWaterInstance.start();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player");
            musicTempleInstance = RuntimeManager.CreateInstance(musicTempleRef);
            musicTempleInstance.start();
        }
    }

   private void OnDisable()
    {
        ambienceInstance.release();
        musicUnderWaterInstance.release();
        musicTempleInstance.release();
    }
}
