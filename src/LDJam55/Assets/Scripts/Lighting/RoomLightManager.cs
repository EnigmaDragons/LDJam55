using UnityEngine;

public class RoomLightManager : OnMessage<ChangeRoomLighting>
{
    [SerializeField] private Color fullyDark;
    [SerializeField] private Color partiallyLit;
    [SerializeField] private Color fullyLit;
    
    private void Start()
    {
        RenderSettings.ambientLight = fullyLit;
    }

    protected override void Execute(ChangeRoomLighting msg)
    {
        var lightingColor = fullyDark;
        if (msg.NumLights > 0)
            lightingColor = partiallyLit;
        if (msg.NumLights > 1)
            lightingColor = fullyLit;

        RenderSettings.ambientLight = lightingColor;
    }
}
