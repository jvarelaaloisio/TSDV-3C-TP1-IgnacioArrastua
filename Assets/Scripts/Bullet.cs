using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool isActive;
    public static float velocity = 50f;
    private static float damage = 30f;
    public static float maxAliveTime = 3f;
    private float timer;

    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        timer = 0.0f;
        switch (gameObject.tag)
        {
            case "PlayerBullet":
                direction = Vector3.forward;
                break; 
            case "EnemyBullet":
                direction = Vector3.back;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
