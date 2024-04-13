using UnityEngine;
using UnityEngine.Events;

public class Triggerable : MonoBehaviour, Constraint
{
    [SerializeField] private bool isTriggered;
    [SerializeField] private bool canBeUntriggered;
    [SerializeField] private bool startsTriggered;
    [SerializeField] private UnityEvent onTrigger;

    public bool IsTriggered => isTriggered;
    public bool IsSatisfied => IsTriggered;

    private void Start()
    {
        if (startsTriggered)
            Trigger();
    }

    public void Trigger()
    {
        isTriggered = true;
        onTrigger.Invoke();
    }

    public void UnTrigger()
    {
        if (!canBeUntriggered)
            return;

        isTriggered = false;
    }
}
