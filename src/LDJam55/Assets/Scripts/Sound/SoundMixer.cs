using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMixer : MonoBehaviour
{
    public bool muteAll;
    public float musicVol = 0.5f;
    void Start()
    {
        InitFMODMixer();
        muteAll = false;
    }

    private void InitFMODMixer()
    {
        RuntimeManager.GetBus("bus:/MST_BUS/DX_MST").setVolume(PlayerPrefs.GetFloat("bus:/MST_BUS/DX_MST", 0.5f));
        RuntimeManager.GetBus("bus:/MST_BUS/ENV_MST").setVolume(PlayerPrefs.GetFloat("bus:/MST_BUS/ENV_MST", 0.5f));
        RuntimeManager.GetBus("bus:/MST_BUS/MUSIC_MST").setVolume(PlayerPrefs.GetFloat("bus:/MST_BUS/MUSIC_MST", 0.5f));
        RuntimeManager.GetBus("bus:/MST_BUS/SFX_MST").setVolume(PlayerPrefs.GetFloat("bus:/MST_BUS/SFX_MST", 0.5f));
        RuntimeManager.GetBus("bus:/MST_BUS").setVolume(PlayerPrefs.GetFloat("bus:/MST_BUS", 0.5f));
    }


    private void Update()
    {
        if(muteAll) 
        {
            RuntimeManager.GetBus("bus:/MST_BUS").setVolume(0f);
        }
        if (!muteAll)
        {
            RuntimeManager.GetBus("bus:/MST_BUS").setVolume(0.5f);
        }

        musicVol = Mathf.Clamp(musicVol, 0f, 0.5f);
        RuntimeManager.GetBus("bus:/MST_BUS/MUSIC_MST").setVolume(musicVol);
    }
}
