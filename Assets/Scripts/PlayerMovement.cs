using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField]
    Transform playerModel;
    [SerializeField]
    Transform rayPosition;
    [SerializeField]
    private Transform aimTarget;
    public Bullet bullet;
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
    public int raycastDistance;
    private bool isFiringRay;

    void Start()
    {
        dolly.m_Speed = cartSpeed;
    }


    void Update()
    {
        Movement();
        //   DebugManager.Instance.Log(this.tag, "Its Activated");


    }

    private void Movement()
    {
        LocalMove(movevementValue.x, movevementValue.y);
        RotationLook(movevementValue.x, movevementValue.y, lookSpeed);
        HorizontalLean(playerModel, movevementValue.y, leanLimit, .1f);
        ClampPosition();
    }

    public void OnMove(InputValue input)
    {

        movevementValue = input.Get<Vector2>();


    }

    public void OnFire(InputValue input)
    {
        ShootBullet();
        //DebugManager.Instance.Log(this.tag, "Apretaste el gatillo. Fire!!");
    }
    public void OnFire2(InputValue input)
    {
        ShootRay();

    }

    private void ShootRay()
    {
        RaycastHit hit;
        if (CheckLaserHitBox(out hit) && hit.collider.CompareTag("Enemy") && hit.collider.GetComponent<EnemyController>().isActive)
        {
            hit.collider.GetComponent<EnemyController>().CurrentHealth -= hit.collider.GetComponent<EnemyController>().CurrentHealth;
            isFiringRay = true;
        }
        else
        {
            isFiringRay = false;
        }
        DebugManager.Instance.Log(tag, isFiringRay.ToString());
    }

    private bool CheckLaserHitBox(out RaycastHit hit)
    {
        return Physics.Raycast(rayPosition.position, rayPosition.forward, out hit, raycastDistance) ||
               Physics.Raycast(rayPosition.position + Vector3.up / 2, rayPosition.forward , out hit, raycastDistance) ||
               Physics.Raycast(rayPosition.position + Vector3.down / 2, rayPosition.forward , out hit, raycastDistance) ||
               Physics.Raycast(rayPosition.position + Vector3.right / 2, rayPosition.forward , out hit, raycastDistance) ||
               Physics.Raycast(rayPosition.position + Vector3.left / 2, rayPosition.forward , out hit, raycastDistance);
    }

    private void ShootBullet()
    {
        bullet.SetStartPosition(transform);
        bullet.SetActiveState(true);
        bullet.ResetBulletTimer();
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

        Gizmos.color = Color.red;
        if (isFiringRay)
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawRay(rayPosition.position,  transform.forward * raycastDistance);
        Gizmos.DrawRay(rayPosition.position + Vector3.up / 2,  rayPosition.forward * raycastDistance);
        Gizmos.DrawRay(rayPosition.position + Vector3.down / 2,  rayPosition.forward * raycastDistance);
        Gizmos.DrawRay(rayPosition.position + Vector3.right / 2, rayPosition.forward * raycastDistance);
        Gizmos.DrawRay(rayPosition.position + Vector3.left / 2,  rayPosition.forward * raycastDistance);
    }

}
