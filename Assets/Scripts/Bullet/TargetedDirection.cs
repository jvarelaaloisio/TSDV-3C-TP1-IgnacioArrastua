using UnityEngine;

[CreateAssetMenu(menuName = "TargetedDirection", fileName = "TargetedDirection")]
public class TargetedDirection : DirectionHandler
{
    public override Vector3 GetDirection(Transform transform, Transform world)
    {
        return world.transform.InverseTransformDirection(transform.forward);
    }
}