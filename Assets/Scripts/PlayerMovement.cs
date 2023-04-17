using System.Collections;
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

    [SerializeField] private ParticleSystem prefire;
    [SerializeField] private ParticleSystem fireLaser;
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
    private bool isPressingButton;
    private float currentBeanTimer;
    [SerializeField]
    private float specialBeanTimer = 1.2f;
    private float specialBeanActiveTime = 1.2f;
    [SerializeField]
    private float minHoldShootTimer = 0.2f;
    private float currentHoldShootTimer;
    [SerializeField]
    private float minShootTimer = 0.05f;
    private float currentSingleShootTimer;

    private bool auxCoroutine = false;
    private bool singleBulletShoot;
    private bool hasPreFireBeenDone;

    void Start()
    {
        dolly.m_Speed = cartSpeed;
    }


    void Update()
    {

        Movement();
        AttackLogic();
    }

    private void AttackLogic()
    {
        currentHoldShootTimer += Time.deltaTime;
        currentSingleShootTimer += Time.deltaTime;
        if (isPressingButton)
        {
            currentBeanTimer += Time.deltaTime;
            if (currentSingleShootTimer > minShootTimer && !singleBulletShoot)
            {
                singleBulletShoot = true;
                currentSingleShootTimer = 0.0f;
                ShootBullet();
            }
            else if (currentHoldShootTimer > minHoldShootTimer && singleBulletShoot)
            {
                ShootBullet();
                currentHoldShootTimer -= minHoldShootTimer;
            }

            if (currentBeanTimer > specialBeanTimer )
            {
                prefire.Play();
                
            }
            else
            {
                prefire.Stop();
            }

        }
        else
        {
            if (currentBeanTimer > specialBeanTimer)
            {
                Debug.Log("Disparo");
                ShootRay();
            }
            singleBulletShoot = false;

            currentBeanTimer = 0.0f;
            currentHoldShootTimer = minHoldShootTimer;
            hasPreFireBeenDone = false;
            prefire.Stop();
        }
    }

    public void ShootCoroutine()
    {
        StartCoroutine(continuosRay());
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

    public void OnFire(InputAction.CallbackContext ctx)
    {
        //  ShootBullet();
        //DebugManager.Instance.Log(this.tag, "Apretaste el gatillo. Fire!!");
        if (ctx.performed)
        {
            isPressingButton = true;

        }
        else if (ctx.canceled)
        {
            isPressingButton = false;
        }
    }
    public void OnFire2(InputAction.CallbackContext ctx)
    {

        ShootRay();

    }


    public void ShootRay()
    {
        fireLaser.Play();
        if (CheckLaserHitBox(out var hit) && hit.collider.CompareTag("Enemy") && hit.collider.GetComponent<EnemyController>().isActive)
        {
            hit.collider.GetComponent<EnemyController>().CurrentHealth -= hit.collider.GetComponent<EnemyController>().CurrentHealth;
            isFiringRay = true;

            Debug.Log("RayoLaser");
        }
        else
        {
            isFiringRay = false;
        }
        // DebugManager.Instance.Log(tag, isFiringRay.ToString());

    }

    IEnumerator continuosRay()
    {
        yield return new WaitForEndOfFrame();
        auxCoroutine = true;
        var currentTimer = 0.0f;
        while (currentTimer < specialBeanActiveTime)
        {
            currentTimer += Time.fixedDeltaTime;

            ShootRay();
        }
        yield break;
        auxCoroutine = false;
    }
    private bool CheckLaserHitBox(out RaycastHit hit)
    {
        return Physics.Raycast(rayPosition.position, rayPosition.forward, out hit, raycastDistance) ||
               Physics.Raycast(rayPosition.position + Vector3.up / 2, rayPosition.forward, out hit, raycastDistance) ||
               Physics.Raycast(rayPosition.position + Vector3.down / 2, rayPosition.forward, out hit, raycastDistance) ||
               Physics.Raycast(rayPosition.position + Vector3.right / 2, rayPosition.forward, out hit, raycastDistance) ||
               Physics.Raycast(rayPosition.position + Vector3.left / 2, rayPosition.forward, out hit, raycastDistance);
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
        Gizmos.DrawRay(rayPosition.position, rayPosition.forward * raycastDistance);
        Gizmos.DrawRay(rayPosition.position + Vector3.up / 2, rayPosition.forward * raycastDistance);
        Gizmos.DrawRay(rayPosition.position + Vector3.down / 2, rayPosition.forward * raycastDistance);
        Gizmos.DrawRay(rayPosition.position + Vector3.right / 2, rayPosition.forward * raycastDistance);
        Gizmos.DrawRay(rayPosition.position + Vector3.left / 2, rayPosition.forward * raycastDistance);
    }

}
