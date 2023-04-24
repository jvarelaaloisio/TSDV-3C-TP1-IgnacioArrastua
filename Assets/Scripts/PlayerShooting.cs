using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts
{
    public class PlayerShooting : MonoBehaviour
    {
        private List<Bullet> _bullets;
        [SerializeField]
        Transform rayPosition;
        [SerializeField]
        private Bullet bullet;
        [SerializeField]
        private ParticleSystem prefire;
        [SerializeField]
        private ParticleSystem fireLaser;
        public int raycastDistance;
        private bool isPressingButton;
        private float currentBeanTimer;
        private bool canFireSpecialBean;
        [SerializeField]
        public float specialBeanCooldown;
        public  float specialBeanCooldownTimer = 0.0f;

        [SerializeField]
        private float specialBeanTimer = 1.2f;
        [SerializeField]
        private float minHoldShootTimer = 0.2f;
        private float currentHoldShootTimer;
        [SerializeField]
        private float minShootTimer = 0.05f;
        private float currentSingleShootTimer;

        private bool singleBulletShoot;
        [SerializeField]
        private Transform bulletHolder;

        private void Update()
        {
            AttackLogic();
        }
        private void AttackLogic()
        {

            SpecialBeanCooldownTimers();
            currentHoldShootTimer += Time.deltaTime;
            currentSingleShootTimer += Time.deltaTime;
            if (isPressingButton)
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

                if (currentBeanTimer > specialBeanTimer && canFireSpecialBean)
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
                if (currentBeanTimer > specialBeanTimer &&canFireSpecialBean)
                {
                    Debug.Log("Disparo");
                    ShootRay();
                    canFireSpecialBean = false;
                    specialBeanCooldownTimer = 0.0f;
                }
                singleBulletShoot = false;
                currentSingleShootTimer = 0.0f;
                currentBeanTimer = 0.0f;
                currentHoldShootTimer = minHoldShootTimer;
                prefire.Stop();
            }
         
        }

        private void SpecialBeanCooldownTimers()
        {
            if (!canFireSpecialBean) specialBeanCooldownTimer += Time.deltaTime;
            if (!(specialBeanCooldownTimer > specialBeanCooldown)) return;
            canFireSpecialBean = true;
      
        }

        public void OnFire(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                isPressingButton = true;

            }
            else if (ctx.canceled)
            {
                isPressingButton = false;
            }
        }
        private void ShootBullet()
        {
            var newBullet = Instantiate(bullet,rayPosition.position, bulletHolder.rotation,bulletHolder);
     
            newBullet.SetStartPosition(transform);
            newBullet.SetActiveState(true);
            newBullet.ResetBulletTimer();
        }
     
        public void ShootRay()
        {
            fireLaser.Play();
            if (CheckLaserHitBox(out var hit) && hit.collider.CompareTag("Enemy") && hit.collider.GetComponent<EnemyController>().isActive)
            {
                hit.collider.GetComponent<EnemyController>().CurrentHealth -= hit.collider.GetComponent<EnemyController>().CurrentHealth;

                Debug.Log("RayoLaser");
            }


        }
        private bool CheckLaserHitBox(out RaycastHit hit)
        {
            return Physics.Raycast(rayPosition.position, rayPosition.forward, out hit, raycastDistance) ||
                   Physics.Raycast(rayPosition.position + Vector3.up / 2, rayPosition.forward, out hit, raycastDistance) ||
                   Physics.Raycast(rayPosition.position + Vector3.down / 2, rayPosition.forward, out hit, raycastDistance) ||
                   Physics.Raycast(rayPosition.position + Vector3.right / 2, rayPosition.forward, out hit, raycastDistance) ||
                   Physics.Raycast(rayPosition.position + Vector3.left / 2, rayPosition.forward, out hit, raycastDistance);
        }
    }
}
