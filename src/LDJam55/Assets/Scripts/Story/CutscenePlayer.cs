using System.Linq;
using FMOD.Studio;
using Story;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//warning hacky garbage
public class CutscenePlayer : OnMessage<PlayCutscene>
{
    [SerializeField] private Image background;
    [SerializeField] private Image protagPortrait;
    [SerializeField] private Image otherPortrait;
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI caption;
    [SerializeField] private StoryCharacter[] allCharacters;
    [SerializeField] private float fmodPullingSeconds;
    [SerializeField] private Button skip;

    private Cutscene _cutscene;
    private int _index;
    private CutsceneSegment _segment;
    private EventInstance _instance;
    private bool _isDelaying;
    private bool _isPlaying;
    private float _delayRemaining;
    private float _fmodT;

    private void Awake()
    {
        _cutscene = null;
        background.gameObject.SetActive(false);
        _isDelaying = false;
        _isPlaying = false;
        skip.onClick.AddListener(() =>
        {
            _instance.stop(STOP_MODE.IMMEDIATE);
        });
    }

    private void Update()
    {
        if (_cutscene == null)
            return;
        if (_isDelaying)
        {
            _delayRemaining -= Time.deltaTime;
            if (_delayRemaining > 0)
                return;
            _isDelaying = false;
            PlayCurrentSegment();
            _isPlaying = true;
        }
        else if (_isPlaying)
        {
            _fmodT += Time.deltaTime;
            if (_fmodT < fmodPullingSeconds)
                return;
            _fmodT -= fmodPullingSeconds;
            if (IsPlaying(_instance))
                return;
            _index++;
            if (_cutscene.Segments.Length == _index)
            {
                _isPlaying = false;
                _cutscene = null;
                background.gameObject.SetActive(false);
                Message.Publish(new CutsceneFinished());
            }
            else
            {
                _segment = _cutscene.Segments[_index];
                _delayRemaining = _segment.Delay;
                _isDelaying = true;
                _isPlaying = false;
            }
        }
    }
    
    protected override void Execute(PlayCutscene msg)
    {
        if (_isPlaying)
            _instance.stop(STOP_MODE.IMMEDIATE);
        if (msg.Cutscene.IsVisualCutscene)
            background.gameObject.SetActive(true);
        _cutscene = msg.Cutscene;
        _index = 0;
        _segment = _cutscene.Segments[_index];
        _delayRemaining = _segment.Delay;
        _isDelaying = true;
        _isPlaying = false;
    }

    private void PlayCurrentSegment()
    {
        background.sprite = _segment.Background;
        var character = allCharacters.First(x => x.VoiceLines.Contains(_segment.VoiceLine));
        if (character.IsProtaganist)
        {
            protagPortrait.sprite = character.CharacterPortrait;
            protagPortrait.gameObject.SetActive(character.CharacterPortrait != null);
            otherPortrait.gameObject.SetActive(false);
        }
        else
        {
            otherPortrait.sprite = character.CharacterPortrait;
            otherPortrait.gameObject.SetActive(character.CharacterPortrait != null);
            protagPortrait.gameObject.SetActive(false);
        }

        characterName.text = character.CharacterName;
        caption.text = _segment.Caption;
        _instance = FMODUnity.RuntimeManager.CreateInstance(_segment.VoiceLine);
        _fmodT = 0;
        _instance.start();
    }
    
    private bool IsPlaying(EventInstance instance)
    {
        instance.getPlaybackState(out var state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}