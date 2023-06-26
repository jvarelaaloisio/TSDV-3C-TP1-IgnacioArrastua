using UnityEngine;

public class BackDirection : DirectionHandler
{
    public override Vector3 GetDirection(Transform transform, Transform world)
    {
        return Vector3.back;
    }
}