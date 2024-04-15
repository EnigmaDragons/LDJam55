using UnityEngine;

public class PlayCutsceneOnEnable : MonoBehaviour
{
    [SerializeField] private Cutscene cutscene;

    private void OnEnable() => Message.Publish(new PlayCutscene(cutscene));
}