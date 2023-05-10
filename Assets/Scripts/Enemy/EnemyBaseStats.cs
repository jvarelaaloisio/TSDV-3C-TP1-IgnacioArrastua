using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseStats : MonoBehaviour
{
    public bool isActive;
    private BoxCollider bc;
    [SerializeField]
    GameObject model;
    [SerializeField]
    private float maxHealth;
    private float _currentHealth;
    private ParticleSystem boom;

    public float CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    void Start()
    {
        isActive = true;
        boom = GetComponentInChildren<ParticleSystem>();
        boom.enableEmission = false;
        bc = GetComponent<BoxCollider>();
        CurrentHealth = maxHealth;
        boom.Stop();
    }

    void Update()
    {
    }
    public void StartObject()
    {
        model.SetActive(true);
        CurrentHealth = maxHealth;
        isActive = true;
      
    }
    private void OnTriggerEnter(Collider other)
    {
    
        if (other.gameObject.tag == "PlayerBullet" && isActive)
        {
            other.gameObject.GetComponent<Bullet>().ResetBulletTimer();
            other.gameObject.GetComponent<Bullet>().SetStartPosition(Vector3.zero);
            other.gameObject.GetComponent<Bullet>().SetActiveState(false);
            CurrentHealth -= other.gameObject.GetComponent<Bullet>().GetDamage();
            CheckEnemyStatus();
        }
    }
    void CheckEnemyStatus()
    {
        if (CurrentHealth <= 0)
        {
            KillEnemy();
            if (boom.isPlaying == false)
            {
                boom.Play();
                Invoke("StartObject", 3f);
            }
        }
    }

    public bool isAlive()
    {
        return (CurrentHealth > 0);
    }
    void KillEnemy()
    {
        model.SetActive(false);
        isActive = false;
    }
}

