using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 scaleWhenOpen = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 scaleWhenClosed = Vector3.one;
    [SerializeField] private bool scaleForOpen = true;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private bool startsOpens = false;
    [SerializeField] private Transform doorScaleTarget;
    [SerializeField] private float animDurationSeconds = 1.5f;

    public bool IsOpen => isOpen;
    
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
        if (scaleForOpen)
            doorScaleTarget.DOScale(scaleWhenOpen, animDurationSeconds);
        doorScaleTarget.DOLocalMoveY(-0.1f, animDurationSeconds);
    }

    public void Close()
    {
        if (!isOpen)
            return;

        isOpen = false;
        if (scaleForOpen)
            doorScaleTarget.DOScale(scaleWhenClosed, animDurationSeconds);
        doorScaleTarget.DOLocalMoveY(0.1f, animDurationSeconds);
    }
}
