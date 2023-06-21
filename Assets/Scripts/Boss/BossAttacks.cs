using System.Linq;
using UnityEngine;

/// <summary>
/// Class for the Boss attacks
/// </summary>
public class BossAttacks : MonoBehaviour
{
    //TODO: TP2 - Syntax - Fix formatting
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform bulletHolder;
    [SerializeField] private Transform shootingPoints;
    Transform[] bulletPoint;
    [SerializeField] Transform playerTransforms;
    [SerializeField] BossMovement bossMovement;
    [SerializeField] private GameObject ray;
    [Header("Cooldowns Presets")]
    [SerializeField] private float shootBulletCooldown = 0.2f;
    private float currentShootBulletCooldown = 0.0f;
    [SerializeField] private float bulletAttackMaxDuration = 0.2f;
    private float currentBulletAttack = 0.0f;
    [SerializeField] private float chooseAttackCooldown = 0.2f;
    private float currentChooseAttack = 0.0f;
    private bool isAttacking;
    private bool isActive = false;
    private bool isAlive;
    private EnemyBaseStats _enemyBaseStats;
    [SerializeField] private Transform World;
    private int attackNumber;


    private void Awake()
    {
        _enemyBaseStats = GetComponent<EnemyBaseStats>();
        bossMovement = GetComponent<BossMovement>();
    }

    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)
    private void Start()
    {

        bulletPoint = shootingPoints.transform.Cast<Transform>().ToArray();
        isActive = true;
        isAttacking = false;
        attackNumber = -1;
    }

    private void Update()
    {
        if (LevelController.levelStatus != LevelController.LevelState.playing) return;
        isAlive = _enemyBaseStats.IsAlive();
        if (isActive && isAlive)
        {
            ShootBulletAttack();

        }
    }
    //TODO: TP2 - Syntax - Fix declaration order
    /// <summary>
    /// Logic for shooting bullets
    /// One bullet for each bulletPoint
    /// Bullets spawn lookit at the player
    /// </summary>
    private void ShootBulletAttack()
    {

        currentShootBulletCooldown += Time.deltaTime;
        if (currentShootBulletCooldown > shootBulletCooldown)
        {
            foreach (var shoot in bulletPoint)
            {
                shoot.LookAt(playerTransforms.position);
                ShootBullet(shoot);
            }

            currentShootBulletCooldown -= shootBulletCooldown;
        }

    }
    //TODO: TP2 - Remove unused methods
    /// <summary>
    /// Logic for the Laser Attack 
    /// </summary>
    private void ShootLaserAttack()
    {
        ShootRay();
        if (bossMovement.hasMovementEnded())
        {
            isAttacking = false;
            ray.SetActive(false);
        }
    }
    //TODO: TP2 - SOLID
    /// <summary>
    /// Choose between different attacks
    /// </summary>
    private void ChooseAttack()
    {
        attackNumber = Random.Range(0, 2);
        switch (attackNumber)
        {
            case 0:
                currentBulletAttack = 0;
                break;
            case 1:
                bossMovement.StartNewPattern();
                break;
        }

        isAttacking = true;
        currentChooseAttack = 0;
    }
    /// <summary>
    /// Intantiate a Bullet in the <paramref name="shooting"/> position
    /// </summary>
    /// <param name="shooting">Transform from where the bullet spawn</param>
    private void ShootBullet(Transform shooting)
    {
        var newBullet = Instantiate(bullet, shooting.position, shooting.rotation, bulletHolder);
        var currentDirection = (World.transform.InverseTransformDirection(shooting.forward));

        newBullet.SetStartPosition(shooting);
        newBullet.SetActiveState(true);
        newBullet.ResetBulletTimer();
        newBullet.SetWorld(World);
        newBullet.SetDirection(currentDirection);
    }
    /// <summary>
    /// Sets the ray prefab to active
    /// </summary>
    private void ShootRay()
    {
        ray.SetActive(true);
    }
    /// <summary>
    /// Sets the parent of the bullets created in ShootBullet
    /// </summary>
    /// <param name="holder">Transform parent for the bullets</param>
    public void SetBulletHolder(Transform holder)
    {
        bulletHolder = holder;
    }
}
