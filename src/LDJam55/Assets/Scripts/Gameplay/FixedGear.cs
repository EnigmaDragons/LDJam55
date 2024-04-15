using System;
using DG.Tweening;
using UnityEngine;

public class FixedGear : ConstraintBase
{
    [SerializeField] private bool rotateObjectOnCrank = true;
    [SerializeField] private bool triggerOnCrank = false;
    [SerializeField] private float rotateDuration = 1f;
    [SerializeField] private Transform gearSnapPoint;

    private bool isTriggered = false;
    private bool isRotating = false;

    public Vector3 GearSnapPosition => gearSnapPoint.position;

    private void OnEnable()
    {
        CurrentGameState.UpdateState(gs => gs.Gears.Add(this));
    }

    private void OnDisable()
    {
        CurrentGameState.UpdateState(gs => gs.Gears.Remove(this));
    }

    public override bool IsSatisfied => isTriggered;

    public void Rotate(Action onFinished)
    {
        if (isRotating)
            return;

        Log.Info("Gear - Rotate", this);
        isRotating = true;
        if (triggerOnCrank)
            isTriggered = true;
        if (rotateObjectOnCrank)
            transform.DORotate(new Vector3(0, 0, 90), rotateDuration, RotateMode.LocalAxisAdd);
        this.ExecuteAfterDelay(() =>
        {
            Log.Info("Gear - Finished Spin", this);
            isRotating = false;
            onFinished();
        }, rotateDuration);
    }
}
