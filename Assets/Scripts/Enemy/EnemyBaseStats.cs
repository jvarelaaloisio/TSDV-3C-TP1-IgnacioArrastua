using UnityEngine;

/// <summary>
/// Class for the EnemyBaseStats
/// </summary>
public class EnemyBaseStats : MonoBehaviour
{
    //TODO: TP2 - Syntax - Consistency in naming convention
    //TODO: TP2 - Syntax - Fix declaration order
    public bool isActive;
    private BoxCollider bc;
    [SerializeField]
    GameObject model;
    [SerializeField]
    private float maxHealth;
    private float _currentHealth;
    private ParticleSystem boom;
    [SerializeField] private ParticleSystem impactPrefab;
    [SerializeField] private int scoreValue;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] [Range(0, 1)] private float explosionVolume;
    [SerializeField] private AudioClip inpactSound;
    [SerializeField] [Range(0, 1)] private float inpactVolume;


    public float CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }

    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)
    void Start()
    {
        isActive = true;
        boom = GetComponentInChildren<ParticleSystem>();
        boom.enableEmission = false;
        bc = GetComponent<BoxCollider>();
        CurrentHealth = maxHealth;
        boom.Stop();

    }


    //TODO: TP2 - Remove unused methods
    /// <summary>
    /// Start enemy object with mesh
    /// Sets current Health to maxHealth and isActive to true
    /// </summary>
    public void StartObject()
    {
        model.SetActive(true);
        CurrentHealth = maxHealth;
        isActive = true;

    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out Bullet bullet) || !isActive) 
            return;

        bullet.ResetBulletTimer();
        bullet.SetStartPosition(Vector3.zero);
        bullet.SetActiveState(false);
        CurrentHealth -= bullet.GetDamage();
        Instantiate(impactPrefab, transform.position, Quaternion.identity, transform);
        SoundManager.Instance.PlaySound(inpactSound, inpactVolume);
        CheckEnemyStatus();
    }
    //TODO: TP2 - Syntax - Fix formatting
    /// <summary>
    /// Check if enemy is Dead
    /// </summary>
    public void CheckEnemyStatus()
    {
        if (!IsAlive())
        {
            KillEnemy();
            PlayerController.Score += scoreValue;
            if (boom.isPlaying == false)
            {
                boom.Play();
                //TODO - Fix - Hardcoded value
                Invoke(nameof(DeActivateEnemy), 1f);
                SoundManager.Instance.PlaySound(explosionSound, explosionVolume);
            }
        }
    }
    /// <summary>
    /// Deactivates the gameObject
    /// </summary>
    public void DeActivateEnemy()
    {
        transform.gameObject.SetActive(false);
    }
    /// <summary>
    /// Returns true if Enemy is Alive
    /// </summary>
    /// <returns></returns>
    public bool IsAlive()
    {
        return (CurrentHealth > 0);
    }
    /// <summary>
    /// Deactivates the model and the active state
    /// </summary>
    private void KillEnemy()
    {
        model.SetActive(false);
        isActive = false;
    }
    //TODO - Fix - Should be native getter
    /// <summary>
    /// Get maxHealth Points
    /// Used for the Boss HealthBar
    /// </summary>
    /// <returns></returns>
    public float GetMaxHealthPoints()
    {
        return maxHealth;
    }
    /// <summary>
    /// Get currentHealth points
    /// Used for the Boss HealthBar
    /// </summary>
    /// <returns></returns>
    public float GetCurrentHealthPoints()
    {
        return CurrentHealth;
    }
}

