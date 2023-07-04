using System.Collections;
using UnityEngine;
/// <summary>
/// Class for the CameraFollow
/// </summary>
[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    [Header("Target")] 
    public Transform target;
    [Header("Offset")] 
    public Vector3 offset = Vector3.zero;
    [Header("Limits")] 
    public Vector2 limits = new(5, 3);

    [Range(0, 1)] public float smoothTime;
    private Vector3 velocity = Vector3.zero;

    private IEnumerator Start()
    {
        while (gameObject.activeSelf)
        {
            yield return null;
            FollowTarget(target);
        }
    }


    private void LateUpdate()
    {
        Vector3 localPos = transform.localPosition;
        float xPosition = Mathf.Clamp(localPos.x, -limits.x, limits.x);
        float yPosition = Mathf.Clamp(localPos.y, -limits.y, limits.y);
        transform.localPosition = new Vector3(xPosition, yPosition, localPos.z);
    }
    /// <summary>
    /// Makes the camera follo the target LocalPosition with an offset and Smooth
    /// </summary>
    /// <param name="target">Transform to follow</param>
    public void FollowTarget(Transform target)
    {
        Vector3 localPos = transform.localPosition;
        Vector3 targetLocalPos = target.transform.localPosition;
        transform.localPosition = Vector3.SmoothDamp(localPos, new Vector3(targetLocalPos.x + offset.x, targetLocalPos.y + offset.y, localPos.z), ref velocity, smoothTime);
    }



}
