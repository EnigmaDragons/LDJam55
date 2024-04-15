using UnityEngine;

public static class TransformExtensions
{
    public static float XzDistanceFromSelf(this Transform self, Transform other)
    {
        var objLoc = new Vector2(other.transform.position.x, other.transform.position.z);
        var selfLoc = new Vector2(self.position.x, self.position.z);
        var distance = Vector2.Distance(objLoc, selfLoc);
        return distance;
    }
}
