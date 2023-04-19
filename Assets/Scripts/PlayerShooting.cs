﻿using System.Collections;
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
        private bool isFiringRay;
        private bool isPressingButton;
        private float currentBeanTimer;
        [SerializeField]
        private float specialBeanTimer = 1.2f;
        [SerializeField]
        private float specialBeanActiveTime = 1.2f;
        [SerializeField]
        private float minHoldShootTimer = 0.2f;
        private float currentHoldShootTimer;
        [SerializeField]
        private float minShootTimer = 0.05f;
        private float currentSingleShootTimer;

        private bool auxCoroutine = false;
        private bool singleBulletShoot;
        [SerializeField]
        private Transform bulletHolder;

        // Use this for initialization
        void Start()
        {

        }
        private void Update()
        {
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
                    //currentSingleShootTimer = 0.0f;
                    ShootBullet();
                }
                else if (currentHoldShootTimer > minHoldShootTimer && singleBulletShoot && currentSingleShootTimer > minHoldShootTimer)
                {
                    ShootBullet();
                    currentHoldShootTimer -= minHoldShootTimer;
                }

                if (currentBeanTimer > specialBeanTimer)
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
                    //StartCorroutine(continuosRay());
                    ShootRay();
                }
                singleBulletShoot = false;
                currentSingleShootTimer = 0.0f;
                currentBeanTimer = 0.0f;
                currentHoldShootTimer = minHoldShootTimer;
                prefire.Stop();
            }
         
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
                isFiringRay = true;

                Debug.Log("RayoLaser");
            }
            else
            {
                isFiringRay = false;
            }
            // DebugManager.Instance.Log(tag, isFiringRay.ToString());

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
