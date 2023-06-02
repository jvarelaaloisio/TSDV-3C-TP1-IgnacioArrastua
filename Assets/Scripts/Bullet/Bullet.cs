using UnityEngine;
/// <summary>
/// Class for the BulletClass
/// </summary>
public class Bullet : MonoBehaviour
{
    //TODO: TP2 - Syntax - Fix declaration order
    private bool isActive;
    public float velocity = 50f;
   [SerializeField] private float damage = 30f;
    public static float maxAliveTime = 3f;
    private float timer;
    private Transform world;

    private Vector3 direction;
    
    //TODO: TP2 - Syntax - Consistency in naming convention
    void Start()
    {
        SetBulletDefaultDirection();
    }
    /// <summary>
    /// Set the default direction of the bullet depending of the BulletGameObject Tag
    /// </summary>
    private void SetBulletDefaultDirection()
    {
        isActive = true;
        timer = 0.0f;
        //TODO: TP2 - SOLID
        switch (gameObject.tag)
        {
            case "PlayerBullet":
            case "BossBullet":
                direction = Vector3.zero;

                break;
            case "EnemyBullet":
                direction = Vector3.back;
                break;
        }
    }
    /// <summary>
    /// Set the bullet direction
    /// The direction is change form world to local
    /// </summary>
    /// <param name="dir">Direction of the bullet</param>
    public void SetDirection(Vector3 dir)
    {
        var aux = transform.InverseTransformDirection(dir);
        direction = aux;
        direction.z = 1;
    
    }
    /// <summary>
    /// Set the World of the bullet
    /// </summary>
    /// <param name="worldTransform">World to use calculations</param>
    public void SetWorld(Transform worldTransform)
    {
        world = worldTransform;
        direction = world.transform.InverseTransformDirection(transform.forward);
        
    }
   
    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)
    void Update()
    {
        //TODO: TP2 - SOLID
        if (gameObject.CompareTag("PlayerBullet") || gameObject.CompareTag("BossBullet"))
        {
            direction = world.transform.InverseTransformDirection(transform.forward);
        }
        //TODO: TP2 - FSM
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
    /// <summary>
    /// Set bullet active status
    /// </summary>
    /// <param name="status">Bullet active state</param>
    public void SetActiveState(bool status)
    {
        isActive = status;
    }
    /// <summary>
    /// Set bullet spawnPosition
    /// </summary>
    /// <param name="spawnPosition">Spawn position of the bullet</param>
    public void SetStartPosition(Transform spawnPosition)
    {
        transform.position = spawnPosition.position;
    }
    /// <summary>
    /// Set bullet spawnPosition
    /// </summary>
    /// <param name="spawnPosition">Spawn position of the bullet</param>
    public void SetStartPosition(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
    }

    /// <summary>
    /// Reset bullet timer
    /// </summary>
    public void ResetBulletTimer()
    {
        timer = 0.0f;
    }
    /// <summary>
    /// Get Bullet Damage
    /// </summary>
    /// <returns></returns>
    public float GetDamage() => damage;
    /// <summary>
    /// Get Active State
    /// </summary>
    /// <returns></returns>
    public bool GetActiveState() => isActive;
}
