using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class FastWaterSound : MonoBehaviour
{

    public EventReference fastWaterRef;
    EventInstance fastWaterInstance;

    public void PlayFastWater()
    {
        fastWaterInstance = RuntimeManager.CreateInstance(fastWaterRef);
        fastWaterInstance.start();
    }

    public void StopFastWater()
    {
        fastWaterInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        fastWaterInstance.release();
    }

}
