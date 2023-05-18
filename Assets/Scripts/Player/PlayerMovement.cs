using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class for the PlayerMovement
/// </summary>

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
    [SerializeField] private AudioClip barrelRollClip;
    [SerializeField] [Range(0, 1)] private float barrelRollVolume;

    [Header("Values")]

    [SerializeField] private float xySpeed;
    [SerializeField] private float lookSpeed;
    [SerializeField] private float cartSpeed;
    [SerializeField] private float leanLimit;
    [SerializeField] private bool isFocusActivate = false;
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
        isFocusActivate = false;
        xySpeed = player.xySpeed;
        lookSpeed = player.lookSpeed;
        cartSpeed = player.cartSpeed;
        maxPositionBeforeClamp = player.maxPositionBeforeClamp;
        minPositionBeforeClamp = player.minPositionBeforeClamp;
    }

    void Update()
    {
       if (LevelController.levelStatus != LevelController.LevelState.playing) return;
            Movement();
    }
    /// <summary>
    /// Logic to make the playerMove
    /// If is Focus is Activated the player will not move but rotate
    /// </summary>
    private void Movement()
    {
        if (!isFocusActivate)
        {
            LocalMove(movevementValue.x, movevementValue.y);
        }
        RotationLook(movevementValue.x, movevementValue.y, lookSpeed);
        HorizontalLean(playerModel, -movevementValue.x, leanLimit, .1f);
        ClampPosition();
    }
    /// <summary>
    /// Changes movevementValue to Input
    /// </summary>
    /// <param name="ctx">Input</param>
    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (Time.timeScale == 0) return;
        movevementValue = ctx.ReadValue<Vector2>();
    }
    /// <summary>
    /// Logic for the RollMovement
    /// Actiavtes the animation
    /// </summary>
    /// <param name="ctx">Input</param>
    public void OnRollInput(InputAction.CallbackContext ctx)
    {
        if (Time.timeScale == 0) return;
        if (ctx.performed && canRoll)
        {
            canRoll = false;
            animator.SetFloat(MovementX, movevementValue.x);
            animator.SetTrigger(IsRolling);
            SoundManager.Instance.PlaySound(barrelRollClip,barrelRollVolume);
        }
    }
    /// <summary>
    /// Activates focusMode
    /// </summary>
    /// <param name="ctx">Input</param>
    public void OnFocusMode(InputAction.CallbackContext ctx)
    {
        isFocusActivate = ctx.performed;
    }
    /// <summary>
    /// Toggle RollState according to bool
    /// </summary>
    /// <param name="state">Bool State</param>
    public void RollMovement(bool state)
    {
        OnRoll?.Invoke(state);
        canRoll = !state;

    }
    /// <summary>
    /// Do Local Move according to the parameters
    /// Can be modifidied with xySpeed and depends on deltaTime
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void LocalMove(float x, float y)
    {
        transform.localPosition += new Vector3(x, y, 0) * xySpeed * Time.deltaTime;
    }
    /// <summary>
    /// Clamps the position of the plauer to not go offlimits
    /// </summary>
    private void ClampPosition()
    {
        var pos = transform.localPosition;
        pos.y = Mathf.Clamp(pos.y, -minPositionBeforeClamp.y, maxPositionBeforeClamp.y);
        pos.x = Mathf.Clamp(pos.x, -minPositionBeforeClamp.x, maxPositionBeforeClamp.x);
        transform.localPosition = new Vector3(pos.x, pos.y, transform.localPosition.z);
    }
    /// <summary>
    /// Changes the rotation of the model
    /// </summary>
    /// <param name="h"></param>
    /// <param name="v"></param>
    /// <param name="speed"></param>
    void RotationLook(float h, float v, float speed)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(h, v, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speed);

    }
    /// <summary>
    /// Leans the player horizontally
    /// </summary>
    /// <param name="target">Target to lean</param>
    /// <param name="axis">Axis to lean</param>
    /// <param name="leanLimit">Limit of the lean</param>
    /// <param name="lerpTime">Time until lean is complete</param>
    void HorizontalLean(Transform target, float axis, float leanLimit, float lerpTime)
    {
        Vector3 targetEulerAngels = target.localEulerAngles;
        target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));
    }


}
