using UnityEngine;

public class ShouldBeFacing : ConstraintBase
{
    [SerializeField] private GameObject target;
    [SerializeField] private float yRotation;
    
    public override bool IsSatisfied => Mathf.Abs(target.transform.eulerAngles.y - yRotation) <= 1;
}
