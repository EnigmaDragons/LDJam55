using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class SoundGuy : MonoBehaviour
{
    public EventReference summonLoopSoundRef;
    public EventReference summonFailedRef;
    public EventReference summonKeyPressedRef;
    EventInstance summonLoopInstance;

    private bool _isSummoning;
    private bool playOnce;
    
    private void OnEnable()
    {
        Message.Subscribe<ShowSummonMenu>(_ => OnSummonStarted(), this);
        Message.Subscribe<HideSummonMenu>(_ => OnSummonStopped(), this);
        Message.Subscribe<SummonFailed>(_ => OnSummonFailed(), this);
    }

    private void OnDisable()
    {
        Message.Unsubscribe(this);
    }

    private void OnSummonStarted()
    {
        _isSummoning = true;
        playOnce = true;
        summonLoopInstance = RuntimeManager.CreateInstance(summonLoopSoundRef);
        summonLoopInstance.start();

        // Code to start the Summon Loop Sound
        // Code to trigger any Summon start oneshots
    }

    private void Update()
    {
        if (_isSummoning) 
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                RuntimeManager.PlayOneShot(summonKeyPressedRef);
            }
        }
        
    }
    private void OnSummonStopped()
    {
        summonLoopInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        summonLoopInstance.release();
        _isSummoning = false;
    }

    private void OnSummonFailed()
    {
        if(playOnce)
        {
            RuntimeManager.PlayOneShot(summonFailedRef);
            playOnce = false;
        }
        
    }
}
