using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
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
        boom = GetComponent<ParticleSystem>();
        boom.enableEmission = true;
        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemyStatus();
    }
    public void StartObject()
    {
        model.SetActive(true);
        isActive = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Bullet" && isActive)
        {
            other.gameObject.GetComponent<Bullet>().ResetBulletTimer();
            other.gameObject.GetComponent<Bullet>().SetStartPosition(Vector3.zero);
            other.gameObject.GetComponent<Bullet>().SetActiveState(false);
            CurrentHealth -= other.gameObject.GetComponent<Bullet>().GetDamage();
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
                CurrentHealth = maxHealth;
                Invoke("StartObject", 3f);
            }
        }
    }
    void KillEnemy()
    {
        model.SetActive(false);
        isActive = false;
    }
}

