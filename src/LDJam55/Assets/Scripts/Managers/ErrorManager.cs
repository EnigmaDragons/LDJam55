using TMPro;
using UnityEngine;

public class ErrorManager : OnMessage<ShowError>
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float secondsToShow;

    private float _t; 
    
    private void Start()
    {
        panel.SetActive(false);
    }

    private void Update()
    {
        if (!panel.activeInHierarchy)
            return;
        _t += Time.deltaTime;
        if (_t >= secondsToShow)
        {
            panel.SetActive(false);
        }
    }

    protected override void Execute(ShowError msg)
    {
        _t = 0;
        text.text = msg.Error;
        panel.SetActive(true);
    }
}