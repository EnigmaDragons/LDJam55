using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class ClockPickupSFX : MonoBehaviour
{
    public EventReference clockSFXRef;
    public void PlayClockSFX()
    {
        RuntimeManager.PlayOneShot(clockSFXRef);
    }
}
