using UnityEngine;

public class ScrollingMatUVs : MonoBehaviour
{
    [SerializeField] private Vector2 uvAnimationRate = new Vector2(1.0f, 0.0f);
    private Vector2 uvOffset = Vector2.zero;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        uvOffset += (uvAnimationRate * Time.deltaTime);
        if (rend != null)
        {
            rend.material.SetTextureOffset("_MainTex", uvOffset);
        }
    }
}
