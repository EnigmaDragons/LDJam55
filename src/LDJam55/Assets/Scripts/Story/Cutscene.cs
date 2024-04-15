using UnityEngine;

[CreateAssetMenu]
public class Cutscene : ScriptableObject
{
    [SerializeField] private CutsceneSegment[] cutsceneSegments;
    [SerializeField] private bool isVisualCutscene = true;

    public CutsceneSegment[] Segments => cutsceneSegments;
    public bool IsVisualCutscene => isVisualCutscene;
}