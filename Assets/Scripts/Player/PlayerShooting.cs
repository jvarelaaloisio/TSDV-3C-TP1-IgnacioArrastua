using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class for the PlayerShooting
/// </summary>

public class PlayerShooting : MonoBehaviour, IFillable
{
    [Header("Channels")]
    [SerializeField] private AskForBulletChannelSO askForBulletChannel;
    [SerializeField] private BoolChannelSO OnFireChannel;
    [SerializeField] private FillUIChannelSO fillUIChannel;
    [Header("Configurations")]
    [SerializeField] private BulletConfiguration bulletConfiguration;
    [SerializeField] private PlayerSettings player;
    [SerializeField] private Transform rayPosition;
    [SerializeField] private ParticleSystem prefireParticle;
    [SerializeField] private Transform[] shootingPoints;
    [SerializeField] private Transform cannon;
    [SerializeField] private GameObject rayObject;

    [Header("Audio")]
    [SerializeField] private AudioClip shootClip;
    [SerializeField] [Range(0, 1)] private float shootVolume;
    [SerializeField] private AudioClip laserClip;
    [SerializeField] [Range(0, 1)] private float laserVolume;
    [SerializeField] private AudioClip prepareLaserClip;
    [SerializeField] [Range(0, 1)] private float prepareLaserVolume;

    [Header("Variables")]
    public int raycastDistance;
    [SerializeField] private Vector3 beamLocalPosition = new(0, 0, 45f);
    [SerializeField] private float beamVisualLifeTime = 0.2f;
    [SerializeField] private float laserDamage = 400f;
    private bool isPressingButton;
    private bool singleBulletShoot;
    [Header("Cooldowns Presets")]
    private float _specialBeanCooldownTimer = 0.0f;
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


    private void Awake()
    {
        shootingPoints = cannon.transform.Cast<Transform>().ToArray();
        OnFireChannel.Subscribe(OnFire);
    }
    private void OnDestroy()
    {
        OnFireChannel.Unsubscribe(OnFire);
    }

    private void Start()
    {
        specialBeanCooldown = player.specialBeanCooldown;
        minShootTimer = player.minShootTimer;
        minHoldShootTimer = player.minHoldShootTimer;
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
        SpecialBeanCooldownTimers();
        currentHoldShootTimer += Time.deltaTime;
        currentSingleShootTimer += Time.deltaTime;
        if (!isPressingButton)
        {
            CheckIfCanFireLaser();
            ResetTimers();
        }
        else
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
                prefireParticle.Play();
                if (!isChargingSpecialBeam)
                    SoundManager.Instance.PlaySound(prepareLaserClip, prepareLaserVolume);

                isChargingSpecialBeam = true;
            }
            else
            {
                prefireParticle.Stop();
            }
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
        prefireParticle.Stop();
    }
    /// <summary>
    /// Checks if player can fireLaser
    /// If true ShootsLaser
    /// </summary>
    private void CheckIfCanFireLaser()
    {
        if (!(currentBeanTimer > specialBeamTimer) || !canFireSpecialBeam) return;
        ShootRay();
        canFireSpecialBeam = false;
        SpecialBeanCooldownTimer = 0.0f;
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
    public void OnFire(bool value) => isPressingButton = value;
    /// <summary>
    /// Logic to Instantiate bullets
    /// The method shoots one bullet for each ShootingPoint
    /// </summary>
    private void ShootBullet()
    {
        SoundManager.Instance.PlaySound(shootClip, shootVolume);
        foreach (Transform shootingPos in shootingPoints)
        {
            askForBulletChannel.RaiseEvent(shootingPos, LayerMask.LayerToName(gameObject.layer), bulletConfiguration, transform.rotation);
        }
    }

    /// <summary>
    /// Instantiate the Shoot Ray Logic
    /// </summary>
    public void ShootRay()
    {
        var newRay = Instantiate(rayObject, rayPosition.position, transform.rotation, transform);
        SoundManager.Instance.PlaySound(laserClip, laserVolume);
        newRay.transform.localPosition += beamLocalPosition;
        if (CheckLaserHitBox(out var hit) && hit.collider.TryGetComponent<EnemyBaseStats>(out var enemyBaseStats) && enemyBaseStats.isActive)
        {
            enemyBaseStats.CurrentHealth -= laserDamage;
            enemyBaseStats.CheckEnemyStatus();
        }
        Destroy(newRay, beamVisualLifeTime);
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
    /// <summary>
    /// Gets the SpecialBeanCooldown max Cooldown
    /// </summary>
    /// <returns></returns>
    public float GetCurrentFillValue()
    {
        return SpecialBeanCooldownTimer;
    }
    /// <summary>
    /// Gets the SpecialBean current cooldown
    /// </summary>
    /// <returns></returns>
    public float GetMaxFillValue()
    {
        return specialBeanCooldown;
    }
}

