using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool isActive;
    public float velocity = 50f;
    private float damage = 30f;
    public static float maxAliveTime = 3f;
    private float timer;
    private Transform world;

    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        timer = 0.0f;
        switch (gameObject.tag)
        {
            case "PlayerBullet":
                direction = Vector3.zero;

                break;
            case "EnemyBullet":
                direction = Vector3.back;
                break;
        }
    }

    public void SetDirection(Vector3 dir)
    {
        var aux = transform.InverseTransformDirection(dir);
        direction = aux;
        direction.z = 1;
    
    }
    public void SetDirection(Transform dir)
    {
        world = dir;
        direction = world.transform.InverseTransformDirection(transform.forward);
        
    }
   
    void Update()
    {
        if (gameObject.CompareTag("PlayerBullet"))
        {
            direction = world.transform.InverseTransformDirection(transform.forward);
        }
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer <= maxAliveTime)
            {
              
                transform.localPosition += Time.deltaTime * velocity * direction;
            }
            else
            {
                isActive = false;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetActiveState(bool status)
    {
        isActive = status;
    }
    public void SetStartPosition(Transform spawnPosition)
    {
        transform.position = spawnPosition.position;
    }
    public void SetStartPosition(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
    }
    public void ResetBulletTimer()
    {
        timer = 0.0f;
    }
    public float GetDamage() => damage;

    public bool GetActiveState() => isActive;
}
