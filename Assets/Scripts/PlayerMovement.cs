using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField]
    Transform playerModel;

    [SerializeField]
    private Transform aimTarget;
    [SerializeField]
    Cinemachine.CinemachineDollyCart dolly;
    [Header("Values")]
    [SerializeField]
    private float xySpeed;
    [SerializeField]
    private float lookSpeed;
    [SerializeField]
    private float cartSpeed;
    private Vector2 movevementValue;
    [SerializeField]
    float leanLimit;
    [Header("ClampValues")]
    [SerializeField]
    Vector2 minPositionBeforeClamp = new Vector2(8,3.5f);
    [SerializeField]
    Vector2 maxPositionBeforeClamp = new Vector2(8, 8);


    void Start()
    {
        dolly.m_Speed = cartSpeed;
    }


    void Update()
    {

        Movement();
    }



    private void Movement()
    {
        LocalMove(movevementValue.x, movevementValue.y);
        RotationLook(movevementValue.x, movevementValue.y, lookSpeed);
        HorizontalLean(playerModel, -movevementValue.x, leanLimit, .1f);
        ClampPosition();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {

        movevementValue = ctx.ReadValue<Vector2>();
    }

    private void LocalMove(float x, float y)
    {
        transform.localPosition += new Vector3(x, y, 0) * xySpeed * Time.deltaTime;
    }
    private void ClampPosition()
    {
        var pos = transform.localPosition;
        pos.y = Mathf.Clamp(pos.y, -minPositionBeforeClamp.y, maxPositionBeforeClamp.y);
        pos.x = Mathf.Clamp(pos.x, -minPositionBeforeClamp.x, maxPositionBeforeClamp.x);
       // pos.x = Mathf.Clamp01(pos.x);
       // pos.y = Mathf.Clamp01(pos.y);
        Debug.Log(pos);
        transform.localPosition = new Vector3(pos.x,pos.y, transform.localPosition.z);
    }
    void RotationLook(float h, float v, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed);

    }
    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
        Gizmos.DrawSphere(aimTarget.position, .15f);
    }

}
