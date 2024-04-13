using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogStart : MonoBehaviour
{
    public bool AllowFog;

    private bool FogOn;

    public GameObject waterLevel;
    
  
 
    void Update()
    {
       if  (transform.position.y> waterLevel.transform.position.y) //fog is turned off.
        {
            AllowFog = false;
        }
        if (transform.position.y < waterLevel.transform.position.y) // under water.. 
        {
            AllowFog = true;
        }
    }


    private void OnPreRender()
    {
        FogOn = RenderSettings.fog;
        RenderSettings.fog = AllowFog; 

    }

    private void OnPostRender()
    {
        RenderSettings.fog = FogOn;
    }
}
