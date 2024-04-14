using System;
using TMPro;
using UnityEngine;

public class ClockUI : OnMessage<SummonLearned, SummonLearningDismissed>
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private float defaultTimeInSeconds = default;
    [SerializeField] private TextMeshProUGUI clockText;
    [SerializeField] private float maxSecondsForPurposesOfCurves;
    [SerializeField] private AnimationCurve scaleByTimeRemaining;
    [SerializeField] private float endScale;
    [SerializeField] private AnimationCurve colorByTimeRemaining;
    [SerializeField] private Color endColor;
    [SerializeField] private ParticleSystem angy;
    [SerializeField] private AnimationCurve angySizeByTimeRemaining;

    [SerializeField] private float introScale;
    [SerializeField] private Vector2 introAnchoredPosition;
    [SerializeField] private float introSecondsBeforeMoving;
    [SerializeField] private float introSecondsToMove;

    private float _startScale;
    private Color _startColor;
    private Vector2 _startPosition;
    
    private bool _hasLost;
    private bool _paused;
    private float _introT;


    private void Start()
    {
        _startScale = rect.localScale.x;
        _startColor = clockText.color;
        _startPosition = rect.anchoredPosition;
        _introT = 0;
        _hasLost = false;
        CurrentGameState.UpdateState(state => state.CurrentGameTime = defaultTimeInSeconds);
        clockText.text = $"{(int)CurrentGameState.GameState.CurrentGameTime / 60}:{(int)CurrentGameState.GameState.CurrentGameTime % 60:00}";
        var main = angy.main;
        main.startColor = new ParticleSystem.MinMaxGradient(new Color(main.startColor.color.r, main.startColor.color.g, main.startColor.color.b, 0));
        rect.localScale = new Vector3(introScale, introScale, introScale);
        rect.anchoredPosition = introAnchoredPosition;
    }

    private void Update()
    {
        if (_hasLost)
            return;

        if (_paused)
            return;
        
        if (_introT < introSecondsBeforeMoving + introSecondsToMove)
        {
            _introT += Time.deltaTime;
            if (_introT >= introSecondsBeforeMoving + introSecondsToMove)
            {
                rect.localScale = new Vector3(_startScale, _startScale, _startScale);
                rect.anchoredPosition = _startPosition;
            }
            else if (_introT > introSecondsBeforeMoving)
            {
                var percent = (_introT - introSecondsBeforeMoving) / introSecondsToMove;
                var scale = Mathf.Lerp(introScale, _startScale, percent);
                var x = Mathf.Lerp(introAnchoredPosition.x, _startPosition.x, percent);
                var y = Mathf.Lerp(introAnchoredPosition.y, _startPosition.y, percent);
                rect.localScale = new Vector3(scale, scale, scale);
                rect.anchoredPosition = new Vector2(x, y);
            }
        }
        else
        {
            CurrentGameState.UpdateState(state => state.CurrentGameTime -= Time.deltaTime);
            if ((int)CurrentGameState.GameState.CurrentGameTime / 60 > 0) 
                clockText.text = $"{(int)CurrentGameState.GameState.CurrentGameTime / 60}:{(int)CurrentGameState.GameState.CurrentGameTime % 60:00}";
            else 
                clockText.text = $"{(int)CurrentGameState.GameState.CurrentGameTime}:{(int)(CurrentGameState.GameState.CurrentGameTime * 1000) % 1000:000}";
            var percent = 1 - Math.Min(maxSecondsForPurposesOfCurves, CurrentGameState.GameState.CurrentGameTime) / maxSecondsForPurposesOfCurves;
            clockText.color = Color.Lerp(_startColor, endColor, colorByTimeRemaining.Evaluate(percent));
            var scale = Mathf.Lerp(_startScale, endScale, scaleByTimeRemaining.Evaluate(percent));
            rect.localScale = new Vector3(scale, scale, scale);
            var main = angy.main;
            main.startColor = new ParticleSystem.MinMaxGradient(new Color(main.startColor.color.r, main.startColor.color.g, main.startColor.color.b, angySizeByTimeRemaining.Evaluate(percent)));

            if(!_hasLost && CurrentGameState.GameState.CurrentGameTime <= 0)
            {
                _hasLost = true;
                Message.Publish(new GameOver());
                clockText.text = "0:000";
            }
        }
    }

    protected override void Execute(SummonLearned msg)
    {
        _paused = true;
    }

    protected override void Execute(SummonLearningDismissed msg)
    {
        _paused = false;
    }
}