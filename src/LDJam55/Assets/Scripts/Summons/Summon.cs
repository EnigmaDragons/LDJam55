using UnityEngine;

[CreateAssetMenu]
public class Summon : ScriptableObject
{

    [SerializeField] private string summonName;
    [SerializeField] private KeyCode[] keyCodes;
    [SerializeField] private Sprite icon;
    [SerializeField] private GameObject summonPrefab;
    [SerializeField] private int manaCost;
    [SerializeField] private string description;

    public string SummonName => summonName;
    public KeyCode[] KeyCodes => keyCodes;
    public Sprite Icon => icon;
    public GameObject SummonPrefab => summonPrefab;
    public int ManaCost => manaCost;
    public string Description => description;
}
