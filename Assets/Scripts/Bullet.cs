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
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;
        timer += Time.deltaTime;
        if (timer <= maxAliveTime)
        {
            transform.localPosition += Vector3.forward * velocity * Time.deltaTime;
        }
        else
        {
            isActive = false;
            timer -= maxAliveTime;
        }
    }
    public void SetActiveState(bool status)
    {
        isActive = status;
    }
    public void SetStartPosition(Transform spawnPosition)
    {
        transform.position = spawnPosition.position;
    } public void SetStartPosition(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
    }
    public void ResetBulletTimer()
    {
        timer = 0.0f;
    }
    public float GetDamage() => damage;
    

}
