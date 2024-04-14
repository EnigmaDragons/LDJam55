
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SummonMenu : OnMessage<SummonLearned, HideSummonMenu, ShowSummonMenu, SummonBegin, SummonRequested, SummonLearningDismissed>
{
    [SerializeField] private List<Summon> summons;
    [SerializeField] private SummonUI summonUIPrefab;
    [SerializeField] private GameObject summonDaddy;
    [SerializeField] private GameObject summonGlyph;
    [SerializeField] private GameObject[] thingsToDisable;

    private Dictionary<string, SummonUI> _summonDic = new();
    private bool _daddyDisabled;

    private bool _isActive;
    private bool _isFrozen;

    private void Awake()
    {
        _summonDic = new();
        foreach (var summon in summons)
        {
            var summonUI = Instantiate(summonUIPrefab, summonDaddy.transform);
            summonUI.gameObject.SetActive(false);
            summonUI.SetSummon(summon);
            _summonDic.Add(summon.SummonName, summonUI);
        }
        summonGlyph.SetActive(false);
    }

    private void Start()
        => SetDaddyStatus();
 


    private void Update()
    {
        if (_daddyDisabled || _isFrozen)
            return;

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            if (_isActive)
                Message.Publish(new HideSummonMenu());
            else
                Message.Publish(new ShowSummonMenu());

        if (_isActive && !_summonDic.Any(x => x.Value.IsActive))
            Message.Publish(new SummonFailed());

    }

    private void ShowActiveSummons()
    {
        foreach (var thing in thingsToDisable)
            thing.SetActive(false);
        foreach (var summon in CurrentGameState.GameState.SummonNames.Where(x => _summonDic.ContainsKey(x)))
            _summonDic[summon].gameObject.SetActive(true);

        summonGlyph.SetActive(true);

        _isActive = true;
    }

    private void HideActiveSummons()
    {
        foreach (var thing in thingsToDisable)
            thing.SetActive(true);
        foreach (var summon in _summonDic)
            summon.Value.gameObject.SetActive(false);
        summonGlyph.SetActive(false);

        _isActive = false;
    }

    protected override void Execute(SummonLearned msg)
        => _isFrozen = true;

    protected override void Execute(SummonLearningDismissed msg)
    {
        if (_isActive)
        {
            ShowActiveSummons();
            return;
        }
        SetDaddyStatus();
        _isFrozen = false;
    }

    private void SetDaddyStatus()
    {
        if (CurrentGameState.GameState.SummonNames.IsNullOrEmpty())
        {
            foreach (var thing in thingsToDisable)
                thing.SetActive(false);
            _daddyDisabled = true;
            return;
        }

        foreach (var thing in thingsToDisable)
            thing.SetActive(true);

        _daddyDisabled = false;
    }

    protected override void Execute(HideSummonMenu msg)
        => HideActiveSummons();

    protected override void Execute(ShowSummonMenu msg)
        => ShowActiveSummons();

    protected override void Execute(SummonBegin msg)
        => _isFrozen = true;

    protected override void Execute(SummonRequested msg)
        => _isFrozen = false;

}
