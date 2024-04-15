using System;
using System.Collections.Generic;
using System.Linq;
using FMOD.Studio;
using FMODUnity;
using Story;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class QuipEngine : OnMessage<PlayCutscene, CutsceneFinished, PlayQuip, TreasureAcquired>
{
    [SerializeField] private EventReference[] pontQuips;
    [SerializeField] private EventReference[] cetoQuips;
    [SerializeField] private EventReference[] thotResponses;

    private bool _treasureAquired;
    private bool _frozen;
    private bool _isPlaying;
    private bool _isResponse;
    private EventInstance _instance;
    
    private List<EventReference> _pontQuipsRemaining;
    private List<EventReference> _cetoQuipsRemaining;
    private List<EventReference> _thotResponsesRemaining;
    
    private void Start()
    {
        _isResponse = false;
        _isPlaying = false;
        _frozen = false;
        _treasureAquired = false;
        _pontQuipsRemaining = pontQuips.Shuffled().ToList();
        _cetoQuipsRemaining = cetoQuips.Shuffled().ToList();
        _thotResponsesRemaining = thotResponses.Shuffled().ToList();
    }

    private void Update()
    {
        if (!_isPlaying)
            return;
        if (IsPlaying(_instance))
            return;
        _isPlaying = false;
        if (!_isResponse && _thotResponsesRemaining.Count > 0)
        {
            var response = _thotResponsesRemaining.First();
            _thotResponsesRemaining.Remove(response);
            _instance = FMODUnity.RuntimeManager.CreateInstance(response);
            _instance.start();
            _isPlaying = true;
            _isResponse = true;
        }
    }
    
    private bool IsPlaying(EventInstance instance)
    {
        instance.getPlaybackState(out var state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
    
    protected override void Execute(PlayCutscene msg)
    {
        _frozen = true;
        if (_isPlaying)
        {
            _instance.stop(STOP_MODE.IMMEDIATE);
            _isPlaying = false;
        }
    }

    protected override void Execute(CutsceneFinished msg)
    {
        _frozen = false;
    }

    protected override void Execute(PlayQuip msg)
    {
        if (_frozen || (_treasureAquired && _cetoQuipsRemaining.Count == 0) || (!_treasureAquired && _pontQuipsRemaining.Count == 0) || _isPlaying)
            return;
        EventReference quip;
        if (_treasureAquired)
        {
            quip = _cetoQuipsRemaining.First();
            _cetoQuipsRemaining.Remove(quip);
        }
        else
        {
            quip = _pontQuipsRemaining.First();
            _pontQuipsRemaining.Remove(quip);
        }

        _isResponse = false;
        _instance = FMODUnity.RuntimeManager.CreateInstance(quip);
        _instance.start();
        _isPlaying = true;
    }

    protected override void Execute(TreasureAcquired msg)
    {
        _treasureAquired = true;
    }
}