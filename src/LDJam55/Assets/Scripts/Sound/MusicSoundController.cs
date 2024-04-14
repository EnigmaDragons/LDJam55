using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSoundController : MonoBehaviour
{

    public EventReference ambienceRef;
    public EventReference musicTempleRef;
    public EventReference musicUnderWaterRef;
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

    public void PlayUnderWater()
    {
        musicTempleInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicTempleInstance.release();
        musicUnderWaterInstance = RuntimeManager.CreateInstance(musicUnderWaterRef);
        musicUnderWaterInstance.start();
        ambienceInstance.setParameterByName("AboveBelowWaterVol", 1);
    }

    public void PlayTemple()
    {
        musicTempleInstance = RuntimeManager.CreateInstance(musicTempleRef);
        musicTempleInstance.start();
        musicUnderWaterInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        musicUnderWaterInstance.release();
        musicUnderWaterInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        //reset ambience param 
        ambienceInstance.setParameterByName("AboveBelowWaterVol", 0);
    }

    private void OnDisable()
    {
        ambienceInstance.release();
        musicUnderWaterInstance.release();
        musicTempleInstance.release();
    }

}
