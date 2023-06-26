using UnityEngine;

public abstract class DirectionHandler : ScriptableObject
{
    public abstract Vector3 GetDirection(Transform transform, Transform world);
}