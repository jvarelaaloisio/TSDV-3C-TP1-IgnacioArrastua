using System.Linq;
using UnityEngine;


/// <summary>
/// Class for the EnemyShooting
/// </summary>
public class EnemyShooting : MonoBehaviour
{
    

    [SerializeField]private Transform shootingPoints;
    [SerializeField] private AskForBulletChannelSO askForBulletChannel;
    [SerializeField] private BulletConfiguration bulletConfiguration;
    private Transform[] bulletPoint;

    [Header("Cooldowns Presets")]
    [SerializeField] private float shootBulletCooldown = 0.2f;
    private float currentShootBulletColdown = 0.0f;
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
        ShootBulletAttack();
    }
    /// <summary>
    /// Logic for shooting bullets according to the timer
    /// One bullet for each bulletPoint
    /// Bullets spawn lookit at the player
    /// </summary>
    private void ShootBulletAttack()
    {
        //TODO: TP2 - FSM
        if (isActive && isAlive)
        {
            currentShootBulletColdown += Time.deltaTime;
            if (currentShootBulletColdown > shootBulletCooldown)
            {
                foreach (var shoot in bulletPoint)
                {
                    ShootBullet(shoot);
                }

                currentShootBulletColdown -= shootBulletCooldown;
            }
        }
    }

    /// <summary>
    /// Intantiate a Bullet in the <paramref name="shooting"/> position
    /// </summary>
    /// <param name="shooting">Transform from where the bullet spawn</param>
    private void ShootBullet(Transform shooting)
    {
        askForBulletChannel.RaiseEvent(shooting,LayerMask.LayerToName(gameObject.layer),bulletConfiguration,shooting.rotation);
    }
 
}
