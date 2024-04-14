using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AmbienceAboveBelowTrigger : MonoBehaviour
{

    public EventReference ambienceRef;
    public EventReference musicUnderWaterRef;
    public EventReference musicTempleRef;
    EventInstance ambienceInstance;
    EventInstance musicUnderWaterInstance;
    EventInstance musicTempleInstance; 

    public MusicSoundController soundController;

    private void Start()
    {

    }

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
