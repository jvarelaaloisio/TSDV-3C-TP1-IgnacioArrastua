using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isActive;
    private BoxCollider bc;
    [SerializeField]
    private float maxHealth;
    private float currentHealth;

    void Start()
    {
        isActive = true;

        bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemyStatus();
    }
    private void OnDisable()
    {
        Invoke("StartObject", 3f);
    }
    private void OnEnable()
    {
        currentHealth = maxHealth;
    }
    public void StartObject()
    {
        this.gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Bullet")
        {
            other.gameObject.GetComponent<Bullet>().ResetBulletTimer();
            other.gameObject.GetComponent<Bullet>().SetStartPosition(Vector3.zero);
            other.gameObject.GetComponent<Bullet>().SetActiveState(false);
            currentHealth -= other.gameObject.GetComponent<Bullet>().GetDamage();


        }
    }
    void CheckEnemyStatus()
    {
        if (currentHealth <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }
}

