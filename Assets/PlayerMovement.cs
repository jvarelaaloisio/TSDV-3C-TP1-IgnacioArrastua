using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform playerModel;
    [SerializeField] private float xySpeed;
    [SerializeField] private float lookSpeed;
    [SerializeField] private float cartSpeed;
    private Vector2 movevementValue;
    [SerializeField] float leanLimit;
    [SerializeField] private Transform aimTarget;
    [SerializeField] Cinemachine.CinemachineDollyCart dolly;



    void Start()
    {
        dolly.m_Speed = cartSpeed;
    }


    void Update()
    {
        LocalMove(movevementValue.x, movevementValue.y);
        RotationLook(movevementValue.x, movevementValue.y, lookSpeed);
       HorizontalLean(playerModel, movevementValue.y, leanLimit, .1f);
        ClampPosition();
     //   DebugManager.Instance.Log(this.tag, "Its Activated");
     

    }

    public void OnMove(InputValue input)
    {

        movevementValue = input.Get<Vector2>();
          
        
    }

    public void OnFire(InputValue input)
    {
        DebugManager.Instance.Log(this.tag, "Apretaste el gatillo. Fire!!");
    }
    private void LocalMove(float x, float y)
    {
        transform.localPosition += new Vector3(x, y, 0) * xySpeed * Time.deltaTime;
    }
    private void ClampPosition()
    {
        var pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
    void RotationLook(float h, float v, float speed)
    {
      //  aimTarget.parent.position = Vector3.zero;
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
