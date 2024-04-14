using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPushable : MonoBehaviour
{
    [SerializeField] private List<Pushable> pushables= new List<Pushable>();

    private void Start(){
        foreach(Pushable p in pushables){
            p.enabled = false;
        }
        SetRandomPushable();
    }

    private void SetRandomPushable(){
        int randomIndex = Random.Range(0, pushables.Count);
        pushables[randomIndex].enabled = true;
    }

}
