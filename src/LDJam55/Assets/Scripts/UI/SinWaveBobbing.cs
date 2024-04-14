using System;
using UnityEngine;

public class SinWaveBobbing : MonoBehaviour
{
    [SerializeField] private RectTransform transform;
    [SerializeField] private float oscillationsPerSecond;
    [SerializeField] private float amplitude;
    [SerializeField] private float tSeed;
    
    private float _t;

    private void Start()
    {
        _t = tSeed;
    }

    private void Update()
    {
        _t += Time.deltaTime;
        var yPosition = amplitude * Mathf.Sin(_t / oscillationsPerSecond);
        transform.anchoredPosition = new Vector2(transform.anchoredPosition.x, yPosition);
    }
}