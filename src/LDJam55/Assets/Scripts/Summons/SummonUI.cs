using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummonUI : OnMessage<SummonBegin>
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

    public bool IsActive;

    private KeyCode[] _summonCode;
    private int _keyIndex;
    private List<Image> _arrows = new();
    private Summon _summon;
    private bool isFrozen;

    public void SetSummon(Summon summon)
    {
        _summon = summon;
        icon.sprite = summon.Icon;
        summonName.text = summon.SummonName;
        _summonCode = summon.KeyCodes;
        ManaCostText.text = "";
        IsActive = false;

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

    protected override void AfterEnable()
    {
        SetActiveState(true);
        _keyIndex = 0;
        isFrozen = false;
    }

    private void Update()
    {
        if (!IsActive || isFrozen)
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
                Message.Publish(new SummonBegin(_summon));
            }
               
            return;
        }

        SetActiveState(false);
    }

    private void SetActiveState(bool isActive)
    {
        IsActive = isActive;
        backdrop.color = isActive ? activeColor : inactiveColor;
        icon.color = isActive ? activeColor : inactiveColor;
        summonName.color = isActive ? activeColor : inactiveColor;
        foreach (Image arrow in _arrows)
            arrow.color = isActive ? activeColor : inactiveColor;
    }

    protected override void Execute(SummonBegin msg)
        => isFrozen = true;

}

