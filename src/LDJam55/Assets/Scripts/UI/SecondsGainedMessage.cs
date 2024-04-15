using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class SecondsGainedMessage : MonoBehaviour
{
    [SerializeField] private RectTransform rect;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float timeAlive;
    [SerializeField] private AnimationCurve opacityOverTime;
    [SerializeField] private float ySpeed;

    private float _t;

    private void Start() => _t = 0;
    
    private void Update()
    {
        _t = Math.Min(timeAlive, _t + Time.deltaTime);
        text.color = new Color(text.color.r, text.color.g, text.color.b, opacityOverTime.Evaluate(_t / timeAlive));
        rect.anchoredPosition = rect.anchoredPosition + new Vector2(0, ySpeed * Time.deltaTime);
        if (_t >= timeAlive)
            Destroy(gameObject);
    }
}