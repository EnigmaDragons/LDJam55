using System;
using FMODUnity;
using UnityEngine;

public class SoundMixer : MonoBehaviour
{
    public bool muteAll = false;
    public float musicVol = 0.5f;
    public float maxVolume = 0.4f;

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
                RuntimeManager.GetBus("bus:/MST_BUS").setVolume(maxVolume);
            }

            musicVol = Mathf.Clamp(musicVol, 0f, 0.3f);
            RuntimeManager.GetBus("bus:/MST_BUS/MUSIC_MST").setVolume(musicVol);
        }
        catch (Exception e)
        {
            Log.Error(e);
            Log.Error("FMod Update Failed");
        }
    }
}
