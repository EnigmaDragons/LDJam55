using DG.Tweening;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 scaleWhenOpen = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 scaleWhenClosed = Vector3.one;
    [SerializeField] private bool scaleForOpen = true;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private bool startsOpens = false;
    [SerializeField] private Transform doorScaleTarget;
    [SerializeField] private float animDurationSeconds = 1.5f;
    [SerializeField] private float delayOpeningSeconds = 1.0f;
    public EventReference doorSuccesSFXRef;
    
    public bool IsOpen => isOpen;
    private bool playonce;

    private void Awake()
    {
        if (startsOpens)
            Open(true);
    }

    private void Start()
    {
        playonce = false;
    }

    public void Open(bool isInitialState = false)
    {
        if (isOpen)
            return;
        this.ExecuteAfterDelay(() =>
        {
            isOpen = true;
            if (!isInitialState)
            {
                if (scaleForOpen)
                    doorScaleTarget.DOScale(scaleWhenOpen, animDurationSeconds);
                doorScaleTarget.DOLocalMoveY(-0.1f, animDurationSeconds);
                if(!playonce)
                {
                    RuntimeManager.PlayOneShot(doorSuccesSFXRef);
                    playonce = true;
                }
            }
            else
            {
                doorScaleTarget.transform.DOScale(scaleWhenOpen, 0.1f);
                doorScaleTarget.DOLocalMoveY(-0.1f, 0.1f);
            }
        },
        delayOpeningSeconds);
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
