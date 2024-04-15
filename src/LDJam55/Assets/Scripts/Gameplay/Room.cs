using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Room : OnMessage<TriggerableChanged>
{
    [SerializeField] private GameObject roof;
    [SerializeField] private UnityEvent onEnter;
    
    private Triggerable[] triggerablesNeededToLightRoom = Array.Empty<Triggerable>();

    private bool _isInThisRoom;
    
    private void Start()
    {
        roof.SetActive(true);
        triggerablesNeededToLightRoom = GetComponentsInChildren<Transform>()
            .Where(child => child.CompareTag("Flammable"))
            .Select(child => child.GetComponent<Triggerable>())
            .Where(triggerable => triggerable != null && triggerable.gameObject.GetComponent<Light>() != null)
            .ToArray();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onEnter.Invoke();
            _isInThisRoom = true;
            roof.gameObject.SetActive(false);
            Message.Publish(new ChangeRoomLighting(triggerablesNeededToLightRoom.Count(x => x.IsTriggered)));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isInThisRoom = false;
            roof.gameObject.SetActive(true);
        }
    }

    protected override void Execute(TriggerableChanged msg)
    {
        if (_isInThisRoom)
            Message.Publish(new ChangeRoomLighting(triggerablesNeededToLightRoom.Count(x => x.IsTriggered)));
    }
}