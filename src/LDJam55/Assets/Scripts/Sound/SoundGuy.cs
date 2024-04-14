using FMODUnity;
using UnityEngine;

public class SoundGuy : MonoBehaviour
{
    public EventReference summonLoopSound;
    public EventReference summonKeyPressed;

    private bool _isSummoning;
    
    private void OnEnable()
    {
        Message.Subscribe<SummonBegin>(_ => OnSummonStarted(), this);
        Message.Subscribe<SummonFailed>(_ => OnSummonFailed(), this);
    }

    private void OnDisable()
    {
        Message.Unsubscribe(this);
    }

    private void OnSummonStarted()
    {
        _isSummoning = true;
        // Code to start the Summon Loop Sound
        // Code to trigger any Summon start oneshots
    }

    private void OnSummonFailed()
    {
        
    }
}
