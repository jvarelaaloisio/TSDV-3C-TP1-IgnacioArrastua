using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrosshair : MonoBehaviour
{
    [SerializeField]
    Transform aimParent;
    Transform initialPosition;
    [SerializeField] 
    private Vector3 velocity = Vector3.zero;
    [SerializeField] 
    private Vector2 offset;

    [Range(0, 1)]
    public float smoothTime;
    private void Start()
    {
        initialPosition = transform;
    }
    private void Update()
    {
       //var aux = Camera.main.WorldToScreenPoint(aimParent.position);
       //transform.position = new Vector3(aux.x, aux.y);
        FollowTarget();
    }

    void FollowTarget()
    {
        Vector3 targetLocalPos = Camera.main.WorldToScreenPoint(aimParent.position);
        Vector3 crossHairPos = transform.position;
        transform.position = Vector3.SmoothDamp(crossHairPos, new Vector3(targetLocalPos.x + offset.x, targetLocalPos.y + offset.y, crossHairPos.z), ref velocity, smoothTime);
    }
}
