using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LearnSummonUI : OnMessage<SummonLearned>
{
    [SerializeField] private RectTransform panel;
    [SerializeField] private TextMeshProUGUI summonName;
    [SerializeField] private TextMeshProUGUI cost;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Image summonIcon;
    [SerializeField] private GameObject objectParent;
    [SerializeField] private GameObject vfx;
    [SerializeField] private Button buttonToDismiss;
    
    [SerializeField] private float _startY;
    [SerializeField] private float _endY;
    [SerializeField] private float _secondsToFlyIn;

    private GameObject summonInstance;
    private bool _showing;
    private float _t;

    private void Awake()
    {
        buttonToDismiss.onClick.AddListener(() =>
        {
            panel.gameObject.SetActive(false);
            vfx.SetActive(false);
            _showing = false;
        });
    }
    
    private void Update()
    {
        if (!_showing || _t >= _secondsToFlyIn)
            return;
        _t = Math.Min(_secondsToFlyIn, _t + Time.deltaTime);
        panel.anchoredPosition = new Vector3(panel.anchoredPosition.x, Mathf.Lerp(_startY, _endY, _t / _secondsToFlyIn));
    }

    protected override void Execute(SummonLearned msg)
    {
        summonName.text = msg.Summon.SummonName;
        cost.text = msg.Summon.ManaCost.ToString();
        description.text = msg.Summon.Description;
        summonIcon.sprite = msg.Summon.Icon;
        panel.anchoredPosition = new Vector3(panel.anchoredPosition.x, _startY);
        panel.gameObject.SetActive(true);
        vfx.SetActive(true);
        summonInstance = Instantiate(msg.Summon.SummonPrefab, objectParent.transform);
        SetGameLayerRecursive(summonInstance, 7);
        _showing = true;
        _t = 0;
    }
    
    private void SetGameLayerRecursive(GameObject gameObject, int layer)
    {
        gameObject.layer = layer;
        foreach (Transform child in gameObject.transform)
        {
            SetGameLayerRecursive(child.gameObject, layer);
        }
    }
}