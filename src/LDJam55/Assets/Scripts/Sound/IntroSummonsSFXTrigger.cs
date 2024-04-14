using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class IntroSummonsSFXTrigger : MonoBehaviour
{
    public EventReference summonsSFXRef;
    EventInstance summonsSFXInstance; 

    void Start()
    {
        summonsSFXInstance = RuntimeManager.CreateInstance(summonsSFXRef);
        summonsSFXInstance.set3DAttributes(RuntimeUtils.To3DAttributes( gameObject));
        summonsSFXInstance.start();
        summonsSFXInstance.setParameterByName("PlaySummonsSuccess", 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            summonsSFXInstance.setParameterByName("PlaySummonsSuccess", 1);
            Invoke("EndEvents", 2f);
            
        }
    }

    void EndEvents()
    {
        summonsSFXInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        summonsSFXInstance.release();
    }

}
