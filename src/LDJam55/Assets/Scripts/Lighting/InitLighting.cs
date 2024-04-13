using UnityEngine;

public class InitLighting : MonoBehaviour
{
    void Awake()
    {
        RenderSettings.ambientLight = new Color(255f/255f, 249f/255f, 242f/255f, 0f);
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
    }
}
