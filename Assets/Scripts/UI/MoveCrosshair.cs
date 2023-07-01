using UnityEngine;
/// <summary>
/// Class for the MoveCrosshair
/// </summary>
public class MoveCrosshair : MonoBehaviour
{
    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)
    [SerializeField]
    Transform aimParent;
    Transform initialPosition;
    [SerializeField]
    private Vector3 velocity = Vector3.zero;
    [SerializeField]
    private Vector2 offset;

    [Range(0, 1)]
    public float smoothTime;

    private Vector3 initialCrosshairPosition;

    private void Start()
    {
        initialCrosshairPosition = transform.position;
        FollowTarget();
    }

    private void Update()
    {
        FollowTarget();
    }
    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)
    /// <summary>
    /// Makes the CrossHair localPosition follow the target relative to the screen point 
    /// </summary>
    private void FollowTarget()
    {
        if (Camera.main != null)
        {
            Vector3 targetLocalPos = Camera.main.WorldToScreenPoint(aimParent.position);
            Vector3 crossHairPos = transform.position;

            //TODO: TP2 - Syntax - Fix formatting
            transform.position = Vector3.SmoothDamp(crossHairPos, new Vector3(targetLocalPos.x + offset.x, targetLocalPos.y + offset.y, crossHairPos.z), ref velocity, smoothTime
            );
        }
    }
    /// <summary>
    /// Resets Crosshair Position to Initalpos and resets velocity
    /// </summary>
    public void ResetCrosshair()
    {
        transform.position = initialCrosshairPosition;
        velocity = Vector3.zero;
    }
}

