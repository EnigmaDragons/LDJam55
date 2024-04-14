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
    private bool stopSoundAtStart;

    private void Awake()
    {
        stopSoundAtStart = true;
        if (startsOpens)
            Open();
    }

    private void Start()
    {
        stopSoundAtStart = false;
        Debug.Log(stopSoundAtStart);
    }

    public void Open()
    {
        if (isOpen)
            return;
        this.ExecuteAfterDelay(() =>
        {
            isOpen = true;
            if (scaleForOpen)
                doorScaleTarget.DOScale(scaleWhenOpen, animDurationSeconds);
            doorScaleTarget.DOLocalMoveY(-0.1f, animDurationSeconds);
            if (!stopSoundAtStart)
            {
                RuntimeManager.PlayOneShot(doorSuccesSFXRef);
                Debug.Log("sound should play");
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
