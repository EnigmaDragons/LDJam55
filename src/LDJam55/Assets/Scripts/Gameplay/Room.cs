﻿using System;
using System.Linq;
using UnityEngine;

public class Room : OnMessage<TriggerableChanged>
{
    [SerializeField] private GameObject roof;
    [SerializeField] private Triggerable[] triggerablesNeededToLightRoom = Array.Empty<Triggerable>();

    private bool _isInThisRoom;
    
    private void Start()
    {
        roof.SetActive(true);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
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