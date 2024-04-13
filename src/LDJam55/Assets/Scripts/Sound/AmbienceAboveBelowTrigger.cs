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
        ambienceInstance.setParameterByName("AboveBelowWaterVol", 0);

        musicTempleInstance = RuntimeManager.CreateInstance(musicTempleRef);
        musicTempleInstance.start();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            musicTempleInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            musicTempleInstance.release();
            musicUnderWaterInstance = RuntimeManager.CreateInstance(musicUnderWaterRef);
            musicUnderWaterInstance.start();

            ambienceInstance.setParameterByName("AboveBelowWaterVol", 1);
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            musicTempleInstance = RuntimeManager.CreateInstance(musicTempleRef);
            musicTempleInstance.start();
            ambienceInstance.setParameterByName("AboveBelowWaterVol", 0);
        }
    }

   private void OnDisable()
    {
        ambienceInstance.release();
        musicUnderWaterInstance.release();
        musicTempleInstance.release();
    }
}
