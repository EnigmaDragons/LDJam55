using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class Summon : ScriptableObject
{

    [SerializeField] private string summonName;
    [SerializeField] private KeyCode[] keyCodes;
    [SerializeField] private Sprite icon;
    [SerializeField] private Sprite summonCardArt;
    [SerializeField] private GameObject summonPrefab;
    [SerializeField] private string description;
    [SerializeField] private float fixedYPosition;
    [SerializeField] private bool summonAtFixedYPosition;
    [SerializeField] private bool summonToSnappedPosition;

    public string SummonName => summonName;
    public KeyCode[] KeyCodes => keyCodes;
    public Sprite Icon => icon;
    public Sprite SummonCardArt => summonCardArt;
    public GameObject SummonPrefab => summonPrefab;
    public string Description => description;
    public float FixedYPosition => fixedYPosition;
    public bool SummonAtFixedYPosition => summonAtFixedYPosition;
    public bool SummonToSnappedPosition => summonToSnappedPosition;
}
