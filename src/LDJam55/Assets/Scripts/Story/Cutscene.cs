using UnityEngine;

[CreateAssetMenu]
public class Cutscene : ScriptableObject
{
    [SerializeField] private CutsceneSegment[] cutsceneSegments;

    public CutsceneSegment[] Segments => cutsceneSegments;
}