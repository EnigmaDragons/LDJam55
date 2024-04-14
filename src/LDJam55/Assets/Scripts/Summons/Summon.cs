using UnityEngine;

[CreateAssetMenu]
public class Summon : ScriptableObject
{

    [SerializeField] private string summonName;
    [SerializeField] private KeyCode[] keyCodes;
    [SerializeField] private Sprite icon;
    [SerializeField] private Sprite summonCardArt;
    [SerializeField] private GameObject summonPrefab;
    [SerializeField] private int manaCost;
    [SerializeField] private float fixedYPosition;
    [SerializeField] private bool summonAtFixedYPosition;

    public string SummonName => summonName;
    public KeyCode[] KeyCodes => keyCodes;
    public Sprite Icon => icon;
    public Sprite SummonCardArt => summonCardArt;
    public GameObject SummonPrefab => summonPrefab;
    public int ManaCost => manaCost;
    public float FixedYPosition => fixedYPosition;
    public bool SummonAtFixedYPosition => summonAtFixedYPosition;
}
