using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public static event Action<bool> OnRoll;

    private bool canRoll = false;
    private bool canMove = false;

    [Header("GameObjects")]

    [SerializeField] private PlayerSettings player;
    [SerializeField] Transform playerModel;

    [SerializeField] private Animator animator;
    [SerializeField] private Transform aimTarget;
    [SerializeField] Cinemachine.CinemachineDollyCart dolly;
    
    [Header("Values")]

    [SerializeField] private float xySpeed;
    [SerializeField] private float lookSpeed;
    [SerializeField] private float cartSpeed;
    [SerializeField] private float leanLimit;
    private Vector2 movevementValue;



    [Header("ClampValues")] 
    private Vector2 minPositionBeforeClamp;
    private Vector2 maxPositionBeforeClamp;
    private static readonly int IsRolling = Animator.StringToHash("IsRolling");
    private static readonly int MovementX = Animator.StringToHash("MovementX");


    private void Start()
    {
        canRoll = true;
        canMove = true;
        dolly.m_Speed = cartSpeed;
        xySpeed = player.xySpeed;
        lookSpeed = player.lookSpeed;
        cartSpeed = player.cartSpeed;
        maxPositionBeforeClamp = player.maxPositionBeforeClamp;
        minPositionBeforeClamp = player.minPositionBeforeClamp;
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
    public void OnRollInput(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canRoll)
        {
            canRoll = false;
            animator.SetFloat(MovementX, movevementValue.x);
            animator.SetTrigger(IsRolling);
        }
    }

    public void RollMovement(bool state)
    {
        OnRoll?.Invoke(state);
        canRoll = !state;
    
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
        transform.localPosition = new Vector3(pos.x, pos.y, transform.localPosition.z);
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(aimTarget.position, .5f);
        Gizmos.DrawSphere(aimTarget.position, .15f);
    }

}
