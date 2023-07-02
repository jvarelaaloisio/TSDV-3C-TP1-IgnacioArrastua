using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class for the PlayerShooting
/// </summary>

public class PlayerShooting : MonoBehaviour, IFillable
{
    //TODO: TP2 - Syntax - Fix declaration order
    //TODO: TP2 - Syntax - Consistency in naming convention
    private bool canShoot;

    [SerializeField] private AskForBulletChannelSO askForBulletChannel;
    [SerializeField] private BulletConfiguration bulletConfiguration;
    [SerializeField] private FillUIChannelSO fillUIChannel;
    [SerializeField] private PlayerSettings player;
    [SerializeField] Transform rayPosition;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform World;
    [SerializeField] private ParticleSystem prefire;
    [SerializeField] private Transform[] shootingPoints;
    [SerializeField] private Transform cannon;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] [Range(0, 1)] private float shootVolume;
    [SerializeField] private float laserDamage = 200f;
    [SerializeField] private AudioClip laserClip;
    [SerializeField] [Range(0, 1)] private float laserVolume;
    [SerializeField] private AudioClip prepareLaserClip;
    [SerializeField] [Range(0, 1)] private float prepareLaserVolume;
    public int raycastDistance;
    private bool isPressingButton;

    [Header("Cooldowns Presets")]
    private float currentBeanTimer;
    private bool canFireSpecialBeam;
    private float specialBeanCooldown;

    public float SpecialBeanCooldownTimer
    {
        get => _specialBeanCooldownTimer;
        set
        {
            _specialBeanCooldownTimer = value;
            fillUIChannel.RaiseEvent(this as IFillable);
        }
    }

    private bool isChargingSpecialBeam = false;
    private float specialBeamTimer = 1.2f;
    private float minHoldShootTimer = 0.2f;
    private float currentHoldShootTimer;
    private float minShootTimer = 0.05f;
    private float currentSingleShootTimer;

    private bool singleBulletShoot;
    [SerializeField]
    private Transform bulletHolder;

    [SerializeField] private GameObject rayObject;
    [SerializeField] private float _specialBeanCooldownTimer = 0.0f;

    private void Awake()
    {
        shootingPoints = cannon.transform.Cast<Transform>().ToArray();
    }

    private void Start()
    {
        canShoot = true;
        specialBeanCooldown = player.specialBeanCooldown;
        minShootTimer = player.minShootTimer;
        minHoldShootTimer = player.minHoldShootTimer;
        PlayerMovement.OnRoll += PlayerMovement_OnRoll;

    }

    private void PlayerMovement_OnRoll(bool isOnRoll)
    {
        canShoot = !isOnRoll;
    }
    private void OnDestroy()
    {
        PlayerMovement.OnRoll -= PlayerMovement_OnRoll;
    }

    private void Update()
    {
        if (LevelController.levelStatus != LevelController.LevelState.playing) return;
        AttackLogic();
    }
    /// <summary>
    /// Player AttackLogic
    /// </summary>
    private void AttackLogic()
    {
        //TODO: TP2 - FSM- Pasarlo a corroutine
        if (!canShoot) return;

        SpecialBeanCooldownTimers();
        currentHoldShootTimer += Time.deltaTime;
        currentSingleShootTimer += Time.deltaTime;
        //TODO: TP2 - SOLID
        if (isPressingButton)
        {
            if (!isChargingSpecialBeam)
            {
                currentBeanTimer += Time.deltaTime;
                if (currentSingleShootTimer > minShootTimer && !singleBulletShoot)
                {
                    singleBulletShoot = true;
                    ShootBullet();
                }
                else if (currentHoldShootTimer > minHoldShootTimer && singleBulletShoot && currentSingleShootTimer > minHoldShootTimer)
                {
                    ShootBullet();
                    currentHoldShootTimer -= minHoldShootTimer;
                }
            }
            if (currentBeanTimer > specialBeamTimer && canFireSpecialBeam)
            {
                prefire.Play();
                if (!isChargingSpecialBeam)
                {
                    SoundManager.Instance.PlaySound(prepareLaserClip, prepareLaserVolume);
                }
                isChargingSpecialBeam = true;
            }
            else
            {
                prefire.Stop();
            }

        }
        else
        {
            CheckIfCanFireLaser();
            ResetTimers();
        }

    }
    /// <summary>
    /// Resets variables for shooting 
    /// </summary>
    private void ResetTimers()
    {
        singleBulletShoot = false;
        currentSingleShootTimer = 0.0f;
        currentBeanTimer = 0.0f;
        currentHoldShootTimer = minHoldShootTimer;
        isChargingSpecialBeam = false;
        prefire.Stop();
    }
    /// <summary>
    /// Checks if player can fireLaser
    /// If true ShootsLaser
    /// </summary>
    private void CheckIfCanFireLaser()
    {
        if (currentBeanTimer > specialBeamTimer && canFireSpecialBeam)
        {

            ShootRay();
            canFireSpecialBeam = false;
            SpecialBeanCooldownTimer = 0.0f;
        }
    }

    /// <summary>
    /// Logic of the timers for the SpecialBean
    /// </summary>
    private void SpecialBeanCooldownTimers()
    {
        if (!canFireSpecialBeam) SpecialBeanCooldownTimer += Time.deltaTime;
        if (!(SpecialBeanCooldownTimer > specialBeanCooldown)) return;
        canFireSpecialBeam = true;

    }
    //TODO - Fix - Using Input related logic outside of an input responsible class
    /// <summary>
    /// InputAction for the ShootInput
    /// </summary>
    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isPressingButton = true;
            Debug.Log("IsPressingButton");
        }
        else if (ctx.canceled)
        {
            isPressingButton = false;
        }
    }
    /// <summary>
    /// Logic to Instantiate bullets
    /// The method shoots one bullet for each ShootingPoint
    /// </summary>
    private void ShootBullet()
    {
        Debug.Log("Shoot");
        SoundManager.Instance.PlaySound(shootClip, shootVolume);
        foreach (Transform shootingPos in shootingPoints)
        {
            askForBulletChannel.RaiseEvent(shootingPos, LayerMask.LayerToName(gameObject.layer), bulletConfiguration, transform.rotation);
        }
    }
    /// <summary>
    /// Gets the SpecialBeanCooldown max Cooldown
    /// </summary>
    /// <returns></returns>
    public float GetSpecialBeanCooldown() => specialBeanCooldown;
    /// <summary>
    /// Gets the SpecialBean current cooldown
    /// </summary>
    /// <returns></returns>
    public float GetSpecialBeanCooldownTimer() => SpecialBeanCooldownTimer;

    /// <summary>
    /// Instantiate the Shoot Ray Logic
    /// </summary>
    public void ShootRay()
    {

        var newRay = Instantiate(rayObject, rayPosition.position, transform.rotation, transform);
        SoundManager.Instance.PlaySound(laserClip, laserVolume);
        newRay.transform.localPosition += new Vector3(0, 0, 45f);
        if (CheckLaserHitBox(out var hit) && hit.collider.CompareTag("Enemy") && hit.collider.GetComponent<EnemyBaseStats>().isActive)
        {
            hit.collider.GetComponent<EnemyBaseStats>().CurrentHealth -= hit.collider.GetComponent<EnemyBaseStats>().CurrentHealth;
            hit.collider.GetComponent<EnemyBaseStats>().CheckEnemyStatus();

        }

        if (CheckLaserHitBox(out hit) && hit.collider.CompareTag("Boss") && hit.collider.GetComponent<EnemyBaseStats>().isActive)
        {
            hit.collider.GetComponent<EnemyBaseStats>().CurrentHealth -= laserDamage;
            hit.collider.GetComponent<EnemyBaseStats>().CheckEnemyStatus();
        }
        Destroy(newRay, 0.2f);
    }
    /// <summary>
    /// Check if Ray hit Something
    /// </summary>
    /// <param name="hit"></param>
    /// <returns></returns>
    private bool CheckLaserHitBox(out RaycastHit hit)
    {
        return Physics.Raycast(rayPosition.position, rayPosition.forward, out hit, raycastDistance) ||
               Physics.Raycast(rayPosition.position + Vector3.up / 2, rayPosition.forward, out hit, raycastDistance) ||
               Physics.Raycast(rayPosition.position + Vector3.down / 2, rayPosition.forward, out hit, raycastDistance) ||
               Physics.Raycast(rayPosition.position + Vector3.right / 2, rayPosition.forward, out hit, raycastDistance) ||
               Physics.Raycast(rayPosition.position + Vector3.left / 2, rayPosition.forward, out hit, raycastDistance);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(rayPosition.position, rayPosition.forward * raycastDistance);
        Gizmos.DrawRay(rayPosition.position + Vector3.up / 2, rayPosition.forward * raycastDistance);
        Gizmos.DrawRay(rayPosition.position + Vector3.down / 2, rayPosition.forward * raycastDistance);
        Gizmos.DrawRay(rayPosition.position + Vector3.right / 2, rayPosition.forward * raycastDistance);
        Gizmos.DrawRay(rayPosition.position + Vector3.left / 2, rayPosition.forward * raycastDistance);
    }

    public float GetCurrentValue()
    {
        return SpecialBeanCooldownTimer;
    }

    public float GetMaxValue()
    {
        return specialBeanCooldown;
    }
}

