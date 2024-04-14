using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class SoundGuy : MonoBehaviour
{
    public EventReference summonLoopSoundRef;
    public EventReference summonKeyPressedRef;
    EventInstance summonLoopInstance;

    private bool _isSummoning;
    
    private void OnEnable()
    {
        Message.Subscribe<ShowSummonMenu>(_ => OnSummonStarted(), this);
        Message.Subscribe<SummonFailed>(_ => OnSummonFailed(), this);
    }

    private void OnDisable()
    {
        Message.Unsubscribe(this);
    }

    private void OnSummonStarted()
    {
        _isSummoning = true;
        summonLoopInstance = RuntimeManager.CreateInstance(summonLoopSoundRef);
        summonLoopInstance.start();
        Debug.Log("SUMMON HAS STARTED");
        // Code to start the Summon Loop Sound
        // Code to trigger any Summon start oneshots
    }

    private void OnSummonFailed()
    {
        
    }
}
