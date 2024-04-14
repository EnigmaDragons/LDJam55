using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Tutorial : OnMessage<SummonLearned, SummonRequested, ShowSummonMenu>
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private string ctrlTutorial;
    [SerializeField] private string keyCodeTutorial;
    [SerializeField] private GameObject[] thingsToDisable;


    private _tutorialName _activeTutorial;
    private HashSet<_tutorialName> _learnedTutorial = new();

    private enum _tutorialName
    {
        None,
        CtrlTutorial,
        KeyCodeTutorial
    }



    private void Start()
    {
        _learnedTutorial = new();
        _activeTutorial = _tutorialName.None;
        SetTutorialState(false, "");
    }

    protected override void Execute(SummonLearned msg)
    {
        if (_activeTutorial  != _tutorialName.None || _learnedTutorial.Any(x=>x == _tutorialName.CtrlTutorial))
            return;
        SetTutorialState(true, ctrlTutorial);
    }

    protected override void Execute(ShowSummonMenu msg)
    {
        if(_activeTutorial != _tutorialName.CtrlTutorial)
        {
            _activeTutorial = _tutorialName.None;
            _learnedTutorial.Add(_tutorialName.CtrlTutorial);
        }

        if (_activeTutorial != _tutorialName.None || _learnedTutorial.Any(x => x == _tutorialName.KeyCodeTutorial))
            return;


        SetTutorialState(true, keyCodeTutorial);
    }

    protected override void Execute(SummonRequested msg)
    {
        if (_activeTutorial != _tutorialName.KeyCodeTutorial)
        {
            _learnedTutorial.Add(_tutorialName.KeyCodeTutorial);
            SetTutorialState(false, "");
        }
    }


    private void SetTutorialState(bool isActive, string activeText)
    {
        tutorialText.text = activeText;
        foreach (var thing in thingsToDisable)
            thing.SetActive(isActive);
    }

}
