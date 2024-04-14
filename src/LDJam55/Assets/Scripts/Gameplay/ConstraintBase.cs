using UnityEngine;

public abstract class ConstraintBase : MonoBehaviour, Constraint
{
    public abstract bool IsSatisfied { get; }
}
