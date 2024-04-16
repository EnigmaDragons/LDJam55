using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class MusicSoundController : MonoBehaviour
{
    public EventReference ambienceRef;
    public EventReference musicTempleRef;
    public EventReference musicUnderWaterRef;
    public EventReference musicCreditsRef;
    public bool playMusicOnStart = true;
    
    EventInstance ambienceInstance;
    EventInstance musicUnderWaterInstance;
    EventInstance musicTempleInstance;
    EventInstance musicCreditsInstance;

    private void Start()
    {
        ambienceInstance = RuntimeManager.CreateInstance(ambienceRef);
        ambienceInstance.start();
        ambienceInstance.setParameterByName("AboveBelowWaterVol", 0);

        if (playMusicOnStart)
        {
            musicTempleInstance = RuntimeManager.CreateInstance(musicTempleRef);
            musicTempleInstance.start();
        }
    }

    public void PlayUnderWater()
    {
        StopAllMusic();
        musicUnderWaterInstance = RuntimeManager.CreateInstance(musicUnderWaterRef);
        musicUnderWaterInstance.start();
        ambienceInstance.setParameterByName("AboveBelowWaterVol", 1);
    }

    public void PlayTemple()
    {
        StopAllMusic();
        musicTempleInstance = RuntimeManager.CreateInstance(musicTempleRef);
        musicTempleInstance.start();
        //reset ambience param 
        ambienceInstance.setParameterByName("AboveBelowWaterVol", 0);
    }

    public void PlayCreditsMusic()
    {
        StopAllMusic();
        musicCreditsInstance = RuntimeManager.CreateInstance(musicCreditsRef);
        musicCreditsInstance.start();
    }

    private void OnDisable()
    {
        ambienceInstance.release();
        musicUnderWaterInstance.release();
        musicTempleInstance.release();
        musicCreditsInstance.release();
    }
    
    private void StopAllMusic()
    {
        StopInstance(musicUnderWaterInstance);
        StopInstance(musicTempleInstance);
        StopInstance(musicCreditsInstance);
    }

    private void StopInstance(EventInstance e)
    {
        e.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        e.release();
    }
}
