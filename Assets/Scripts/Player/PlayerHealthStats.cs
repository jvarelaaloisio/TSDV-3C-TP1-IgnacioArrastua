using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthStats : MonoBehaviour
{

   [SerializeField] private int maxHealthPoints;
    private float currentHealth;
    private BoxCollider bc;
    [SerializeField] private bool isInvincible = false;
    void Start()
    {
        currentHealth = maxHealthPoints;
        bc = GetComponent<BoxCollider>();
    }

    public void ReceiveDamage(float damage)
    {
        currentHealth -= damage;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("EnemyBullet") && !isInvincible)
        {
            Debug.Log("EnemyBulletHit");
            other.gameObject.GetComponent<Bullet>().ResetBulletTimer();
            other.gameObject.GetComponent<Bullet>().SetStartPosition(Vector3.zero);
            other.gameObject.GetComponent<Bullet>().SetActiveState(false);
            ReceiveDamage(other.gameObject.GetComponent<Bullet>().GetDamage());
        }
    }

    public float GetMaxHealthPoints()
    {
        return maxHealthPoints;
    }

    public float GetCurrentHealthPoints()
    {
        return currentHealth;
    }
}
