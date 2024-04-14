using System;
using UnityEngine;

public class RoomLightManager : OnMessage<ChangeRoomLighting>
{
    private void Start()
    {
        RenderSettings.ambientLight = Color.white;
    }

    protected override void Execute(ChangeRoomLighting msg)
    {
        RenderSettings.ambientLight = msg.IsDark ? Color.black : Color.white;
    }
}