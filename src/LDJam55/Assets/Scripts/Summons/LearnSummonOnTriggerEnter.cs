using System;
using UnityEngine;

public class LearnSummonOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private Summon summon;
    [SerializeField] private float delayBeforeDisable = 1f;
    [SerializeField] private SpriteRenderer[] cardViews = Array.Empty<SpriteRenderer>();

    private void Awake()
    {
        Log.Info("Learn Summon - Awake", this);
        cardViews.ForEach(c => c.sprite = summon.SummonCardArt);
    }

    public void OnTriggerEnter(Collider c)
    {
        Log.Info("Trigger Enter", this);
        if (c.gameObject.CompareTag("Player"))
        {
            CurrentGameState.UpdateState(x => x.SummonNames.Add(summon.SummonName));
            Message.Publish(new SummonLearned(summon));
            this.ExecuteAfterDelay(() => transform.gameObject.SetActive(false), delayBeforeDisable);
        }
    }
}
