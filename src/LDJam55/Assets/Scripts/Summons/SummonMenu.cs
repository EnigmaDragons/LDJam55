
using Sirenix.Utilities;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SummonMenu : OnMessage<GameStateChanged, HideSummonMenu, ShowSummonMenu>
{
    [SerializeField] private List<Summon> summons;
    [SerializeField] private SummonUI summonUIPrefab;
    [SerializeField] private GameObject summonDaddy;
    [SerializeField] private GameObject[] thingsToDisable;

    private Dictionary<string, SummonUI> _summonDic = new();
    private bool _daddyDisabled;

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
    }

    private void Start()
        => SetDaddyStatus();
 

    private bool _isActive;

    private void Update()
    {
        if (_daddyDisabled)
            return;

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            if (_isActive)
                Message.Publish(new HideSummonMenu());
            else
                Message.Publish(new ShowSummonMenu());
    }

    private void ShowActiveSummons()
    {
        foreach (var thing in thingsToDisable)
            thing.SetActive(false);
        foreach (var summon in CurrentGameState.GameState.SummonNames.Where(x => _summonDic.ContainsKey(x)))
            _summonDic[summon].gameObject.SetActive(true);

        _isActive = true;
    }

    private void HideActiveSummons()
    {
        foreach (var thing in thingsToDisable)
            thing.SetActive(true);
        foreach (var summon in _summonDic)
            summon.Value.gameObject.SetActive(false);

        _isActive = false;
    }


    protected override void Execute(GameStateChanged msg)
    {
        if (_isActive)
        {
            ShowActiveSummons();
            return;
        }
        SetDaddyStatus();
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
}
