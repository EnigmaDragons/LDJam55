using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AmbienceAboveBelowTrigger : MonoBehaviour
{

    public MusicSoundController soundController;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {   
            soundController.PlayUnderWater();
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "Player")
        {
            soundController.PlayTemple();
        }
    }

   
}
