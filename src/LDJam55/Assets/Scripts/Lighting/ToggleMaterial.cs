using UnityEngine;

public class ToggleMaterial : MonoBehaviour
{
    [SerializeField] private Renderer[] targets;
    [SerializeField] private bool startsOn;
    [SerializeField] private Material onMaterial;
    [SerializeField] private Material offMaterial;
    
    void OnEnable()
    {
        if (startsOn)
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    public void TurnOn()
    { 
        targets.ForEach(t =>  {
          t.material = onMaterial;
        });
    }

    public void TurnOff()
    { 
        targets.ForEach(t =>  {
            t.material = offMaterial;
        });
    }
}
