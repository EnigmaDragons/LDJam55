using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
internal class SummonGlyph : OnMessage<SummonBegin, SummonFailed>
{
    [SerializeField] private List<Image> arrows = new();
    [SerializeField] private Image summonImage;
    [SerializeField] private Sprite arrowUp;
    [SerializeField] private Sprite arrowDown;
    [SerializeField] private Sprite arrowLeft;
    [SerializeField] private Sprite arrowRight;
    [SerializeField] private Sprite failSprite;
    [SerializeField] private float summonDelay;
    [SerializeField] private GameObject growingPower;
    [SerializeField] private float sizePerArrow;

    private int _keyIndex;
    private bool _isFrozen;

    protected override void AfterEnable()
    {
        _keyIndex = 0;
        foreach (var arrow in arrows)
        {
            arrow.gameObject.SetActive(false);
        }
        summonImage.gameObject.SetActive(false);
        _isFrozen = false;
        var main = growingPower.GetComponent<ParticleSystem>().main;
        main.startSize = new ParticleSystem.MinMaxCurve(0);
    }

    private void Update()
    {
        if(_isFrozen || _keyIndex >= arrows.Count)
            return;

        Sprite arrowSprite;

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            arrowSprite = arrowUp;
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            arrowSprite = arrowDown;
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            arrowSprite = arrowLeft;
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            arrowSprite = arrowRight;
        else
            return;

        arrows[_keyIndex].gameObject.SetActive(true);
        arrows[_keyIndex].sprite = arrowSprite;
        _keyIndex++;
        var main = growingPower.GetComponent<ParticleSystem>().main;
        main.startSize = new ParticleSystem.MinMaxCurve(_keyIndex * sizePerArrow);
    }

    protected override void Execute(SummonBegin msg)
    {
        summonImage.sprite = msg.Summon.Icon;
        summonImage.gameObject.SetActive(true);
        StartCoroutine(WaitAndRequestSummon(msg.Summon));    
    }

    protected override void Execute(SummonFailed msg)
    {
        summonImage.sprite = failSprite;
        summonImage.gameObject.SetActive(true);
        StartCoroutine(WaitAndCloseMenu());
    }

    private IEnumerator WaitAndCloseMenu()
    {
        yield return new WaitForEndOfFrame();
        _isFrozen = true;
        yield return new WaitForSeconds(summonDelay);
        Message.Publish(new HideSummonMenu());
    }

    private IEnumerator WaitAndRequestSummon(Summon summon)
    {
        yield return new WaitForEndOfFrame();
        _isFrozen = true;
        yield return new WaitForSeconds(summonDelay);
        Message.Publish(new HideSummonMenu());
        Message.Publish(new SummonRequested(summon));
    }

}

