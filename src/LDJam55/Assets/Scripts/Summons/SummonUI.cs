using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummonUI : MonoBehaviour
{
    [SerializeField] private Image arrowsPrefab;
    [SerializeField] private Image icon;
    [SerializeField] private Image backdrop;
    [SerializeField] private TextMeshProUGUI summonName;
    [SerializeField] private Sprite arrowUp;
    [SerializeField] private Sprite arrowDown;
    [SerializeField] private Sprite arrowLeft;
    [SerializeField] private Sprite arrowRight;
    [SerializeField] private Color activeColor;
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color pressedArrowColor;
    [SerializeField] private GameObject ArrowDaddy;
    [SerializeField] private TextMeshProUGUI ManaCostText;

    private KeyCode[] _summonCode;
    private int _keyIndex;
    private List<Image> _arrows = new();
    private bool _isActive;
    private Summon _summon;


    public void SetSummon(Summon summon)
    {
        _summon = summon;
        icon.sprite = summon.Icon;
        summonName.text = summon.SummonName;
        _summonCode = summon.KeyCodes;
        ManaCostText.text = summon.ManaCost.ToString();

        foreach (KeyCode keyCode in summon.KeyCodes)
        {
            Image arrow = Instantiate(arrowsPrefab, ArrowDaddy.transform);
            arrow.sprite = GetArrowSprite(keyCode);
            _arrows.Add(arrow);
        }
    }

    private Sprite GetArrowSprite(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.UpArrow:
                return arrowUp;
            case KeyCode.DownArrow:
                return arrowDown;
            case KeyCode.LeftArrow:
                return arrowLeft;
            case KeyCode.RightArrow:
                return arrowRight;
            default:
                return _summon.Icon;
        }
    }

    private void OnEnable()
    {
        SetActiveState(_summon.ManaCost <= CurrentGameState.GameState.Mana);
        _keyIndex = 0;
    }

    private void Update()
    {
        if (!_isActive)
            return;

        var key = KeyCode.None;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            key = KeyCode.UpArrow;
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            key = KeyCode.DownArrow;
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            key = KeyCode.LeftArrow;
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            key = KeyCode.RightArrow;
        else
            return;

        if (_summonCode[_keyIndex] == key)
        {
            _arrows[_keyIndex].color = pressedArrowColor;
            _keyIndex++;
            if (_keyIndex >= _summonCode.Length)
            {
                CurrentGameState.UpdateState(x => x.Mana -= _summon.ManaCost);
                Message.Publish(new SummonRequested(_summon));
                Message.Publish(new HideSummonMenu());
            }
               
            return;
        }

        SetActiveState(false);
    }

    private void SetActiveState(bool isActive)
    {
        _isActive = isActive;
        backdrop.color = isActive ? activeColor : inactiveColor;
        icon.color = isActive ? activeColor : inactiveColor;
        summonName.color = isActive ? activeColor : inactiveColor;
        foreach (Image arrow in _arrows)
            arrow.color = isActive ? activeColor : inactiveColor;
    }

}

