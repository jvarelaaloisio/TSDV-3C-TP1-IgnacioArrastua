using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// Class for the MoveCrosshair
/// </summary>
public class MoveCrosshair : MonoBehaviour
{
    
    [Range(0, 1)] public float smoothTime;
    [SerializeField] private Transform aimParent;
    [SerializeField] private Vector2 offset;
    private Vector3 velocity = Vector3.zero;


    /// <summary>
    /// Starts the followTarget one frame later to wait for the UI reset 
    /// </summary>
    /// <returns></returns>
    private IEnumerator Start()
    {
        while (gameObject.activeSelf)
        {
            yield return null;
            FollowTarget();
        }
    }
    /// <summary>
    /// Makes the CrossHair localPosition follow the target relative to the screen point 
    /// </summary>
    private void FollowTarget()
    {
        if (Camera.main == null)
            return;
        Vector3 targetLocalPos = Camera.main.WorldToScreenPoint(aimParent.position);
        Vector3 crossHairPos = transform.position;

        transform.position = Vector3.SmoothDamp(crossHairPos, new Vector3(targetLocalPos.x + offset.x, targetLocalPos.y + offset.y, crossHairPos.z), ref velocity, smoothTime);
    }

}

