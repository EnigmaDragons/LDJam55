using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class SwitchWeigthSFX : MonoBehaviour
{
    public void PlaySwitchSFX()
    {
        RuntimeManager.PlayOneShot("event:/SFX/WeightSwitch");
    }
}
