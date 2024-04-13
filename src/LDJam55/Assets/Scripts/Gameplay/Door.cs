using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 scaleWhenOpen = new Vector3(0, 0, 0);
    [SerializeField] private bool isOpen = false;
    [SerializeField] private bool startsOpens = false;
    [SerializeField] private Transform doorScaleTarget;

    private void Awake()
    {
        if(startsOpens)
            Open();
    }

    public void Open()
    {
        if (isOpen)
            return;
        
        isOpen = true;
        doorScaleTarget.DOScale(scaleWhenOpen, 1.5f);
    }

    public void Close()
    {
        if (!isOpen)
            return;

        isOpen = false;
        doorScaleTarget.DOScale(Vector3.one, 1.5f);
    }
}

