using System;
using FMODUnity;
using UnityEngine;

public class SoundMixer : MonoBehaviour
{
    public bool muteAll = false;
    public float musicVol = 0.5f;

    private bool _isInitialized = false;
    
    void Start() => TryInitFMODMixer();

    private void TryInitFMODMixer()
    {
        if (_isInitialized)
            return;
        
        try
        {
            if (!RuntimeManager.HaveMasterBanksLoaded)
                return;
            
          //  RuntimeManager.GetBus("bus:/MST_BUS/DX_MST").setVolume(PlayerPrefs.GetFloat("bus:/MST_BUS/DX_MST", 0.5f));
           // RuntimeManager.GetBus("bus:/MST_BUS/ENV_MST").setVolume(PlayerPrefs.GetFloat("bus:/MST_BUS/ENV_MST", 0.5f));
            //RuntimeManager.GetBus("bus:/MST_BUS/MUSIC_MST").setVolume(PlayerPrefs.GetFloat("bus:/MST_BUS/MUSIC_MST", 0.5f));
            //RuntimeManager.GetBus("bus:/MST_BUS/SFX_MST").setVolume(PlayerPrefs.GetFloat("bus:/MST_BUS/SFX_MST", 0.5f));
            //RuntimeManager.GetBus("bus:/MST_BUS").setVolume(PlayerPrefs.GetFloat("bus:/MST_BUS", 0.5f));
            _isInitialized = true;
        }
        catch (Exception e)
        {
            Log.Error(e);
            Log.Error("FMod Init Failed");
        }
    }
    
    private void Update()
    {
        try
        {
            if (!_isInitialized)
            {
                TryInitFMODMixer();
                return;
            }

            if (muteAll)
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
        catch (Exception e)
        {
            Log.Error(e);
            Log.Error("FMod Update Failed");
        }
    }
}
