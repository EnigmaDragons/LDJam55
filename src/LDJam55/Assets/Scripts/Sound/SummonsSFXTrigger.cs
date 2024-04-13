
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SummonsSFXTrigger : MonoBehaviour
{
    EventReference summonSFXRef;
    float summonToPlay;

    public enum SummonType
    {
        Summon1,
        Summon2,
        Summon3,
        Summon4,
        Summon5
    }

    public SummonType summonType;

    public void PlaySummonSFX()//var from SummonMenu summon?
    {
        // Use a switch statement based on the enum value
        switch (summonType)
        {
            case SummonType.Summon1:
                
                summonToPlay = 1f;//if poss replace with var from the summonMenu script
                break;
            case SummonType.Summon2:
                
                summonToPlay = 2f;
                break;
            case SummonType.Summon3:
               
                summonToPlay = 3f;
                break;
            case SummonType.Summon4:
              
                summonToPlay = 4f;
                break;
            case SummonType.Summon5:
              
                summonToPlay = 5f;
                break;
            default:
                // Default case
                break;
        }

        //Fmod play sfx
        PlayOneShot_Helper.PlayOneShotWithParameters(summonSFXRef, transform.position, ("SummonsListParam", summonToPlay));
    }
}

