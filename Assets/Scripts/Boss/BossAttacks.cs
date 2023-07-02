using System.Linq;
using UnityEngine;

/// <summary>
/// Class for the Boss attacks
/// </summary>
public class BossAttacks : MonoBehaviour
{

    [SerializeField] private Transform shootingPoints;
    [SerializeField] private AskForBulletChannelSO askForBulletChannel;
    [SerializeField] private BulletConfiguration bulletConfiguration;
    [SerializeField] private Transform playerTransforms;
    private Transform[] bulletPoint;
    [Header("Cooldowns Presets")]
    [SerializeField] private float shootBulletCooldown = 0.2f;
    private float currentShootBulletCooldown = 0.0f;
    private bool isActive = false;
    private bool isAlive;
    private EnemyBaseStats _enemyBaseStats;


    private void Awake()
    {
        _enemyBaseStats = GetComponent<EnemyBaseStats>();
    }

    private void Start()
    {

        bulletPoint = shootingPoints.transform.Cast<Transform>().ToArray();
        isActive = true;
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

    /// <summary>
    /// Intantiate a Bullet in the <paramref name="shooting"/> position
    /// </summary>
    /// <param name="shooting">Transform from where the bullet spawn</param>
    private void ShootBullet(Transform shooting)
    {
        askForBulletChannel.RaiseEvent(shooting, LayerMask.LayerToName(gameObject.layer), bulletConfiguration, shooting.rotation);
    }


}
