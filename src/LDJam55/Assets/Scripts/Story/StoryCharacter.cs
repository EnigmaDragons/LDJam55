using UnityEngine;

[CreateAssetMenu]
public class StoryCharacter : ScriptableObject
{
    [SerializeField] private Sprite characterPortrait;
    [SerializeField] private string characterName;
    [SerializeField] private bool isProtagonist;
    public FMODUnity.EventReference[] VoiceLines = null;
    public Sprite CharacterPortrait => characterPortrait;
    public string CharacterName => characterName;
    public bool IsProtaganist => isProtagonist;
}