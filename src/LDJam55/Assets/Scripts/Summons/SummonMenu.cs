
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SummonMenu : OnMessage<GameStateChanged, HideSummonMenu>
{
    [SerializeField] private List<Summon> summons;
    [SerializeField] private SummonUI summonUIPrefab;
    [SerializeField] private GameObject summonDaddy;
    [SerializeField] private GameObject[] thingsToDisable;

    Dictionary<string, SummonUI> summonDic = new ();

    private void Awake()
    {
        summonDic = new();
        foreach (var summon in summons)
        {
            var summonUI = Instantiate(summonUIPrefab, summonDaddy.transform);
            summonUI.gameObject.SetActive(false);
            summonUI.SetSummon(summon);
            summonDic.Add(summon.SummonName, summonUI);
        }
    }


    private bool _isActive;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
            ShowActiveSummons();
        else if(Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
            HideActiveSummons();
    }

    private void ShowActiveSummons()
    {
        foreach (var thing in thingsToDisable)
            thing.SetActive(false);
        foreach(var summon in CurrentGameState.GameState.SummonNames.Where(x => summonDic.ContainsKey(x)))
                summonDic[summon].gameObject.SetActive(true);

        _isActive = true;
    }

    private void HideActiveSummons()
    {
        foreach (var thing in thingsToDisable)
            thing.SetActive(true);
        foreach (var summon in summonDic)
            summon.Value.gameObject.SetActive(false);

        _isActive = false;
    }

    
    protected override void Execute(GameStateChanged msg)
    {
        if (_isActive)
            ShowActiveSummons();
    }

    protected override void Execute(HideSummonMenu msg)
        => HideActiveSummons();
}
