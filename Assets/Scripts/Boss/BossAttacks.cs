using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossAttacks : MonoBehaviour
{
    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private Transform bulletHolder;
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
    }

    void Start()
    {

        bulletPoint = shootingPoints.transform.Cast<Transform>().ToArray();
        isActive = true;
        isAttacking = false;
        attackNumber = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelController.levelStatus != LevelController.LevelState.playing) return;
        isAlive = _enemyBaseStats.IsAlive();
        if (isActive && isAlive)
        {
            if (!isAttacking)
            {
                currentChooseAttack += Time.deltaTime;
                if (currentChooseAttack > chooseAttackCooldown)
                {
                    ChooseAttack();
                }
            }
            else
            {
                switch (attackNumber)
                {
                    case 0:
                        {
                            ShootBulletAttack();
                        }
                        break;
                    case 1:
                    {
                        ShootLaserAttack();
                    }
                        break;
                }
            }
        }
    }

    private void ShootBulletAttack()
    {
        currentBulletAttack += Time.deltaTime;
        if (currentBulletAttack < bulletAttackMaxDuration)
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
        else
        {
            isAttacking = false;
        }
    }

    private void ShootLaserAttack()
    {
        ShootRay();
        if (bossMovement.hasMovementEnded())
        {
            isAttacking = false;
            ray.SetActive(false);
        }
    }
    private void ChooseAttack()
    {
        attackNumber = Random.Range(0, 1);
        switch (attackNumber)
        {
            case 0:
                currentBulletAttack = 0;
                break;
            case 1:
                bossMovement.StartNewPattern();
                break;
        }

        currentChooseAttack = 0;
    }

    private void ShootBullet(Transform shooting)
    {
        var newBullet = Instantiate(bullet, shooting.position, shooting.rotation, bulletHolder);
        var currentDirection = (World.transform.InverseTransformDirection(shooting.forward));

        newBullet.SetStartPosition(shooting);
        newBullet.SetActiveState(true);
        newBullet.ResetBulletTimer();
        newBullet.SetDirection(World);
        newBullet.SetDirection(currentDirection);
    }

    private void ShootRay()
    {
        ray.SetActive(true);
    }
    public void SetBulletHolder(Transform holder)
    {
        bulletHolder = holder;
    }
}
