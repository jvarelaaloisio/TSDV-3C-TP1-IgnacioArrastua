using UnityEngine;

/// <summary>
/// Class for the EnemyBaseStats
/// </summary>
public class EnemyBaseStats : MonoBehaviour, IFillable
{
    //TODO: TP2 - Syntax - Consistency in naming convention
    //TODO: TP2 - Syntax - Fix declaration order
    [SerializeField] private FillUIChannelSO fillUIChannel;
    public bool isActive;
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
        set
        {
            _currentHealth = value;
            fillUIChannel.RaiseEvent(this as IFillable);
        }
    }

    private void Start()
    {
        isActive = true;
        boom = GetComponentInChildren<ParticleSystem>();
        boom.enableEmission = false;
        CurrentHealth = maxHealth;
        boom.Stop();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent(out Bullet bullet) || !isActive) 
            return;

        CurrentHealth -= bullet.GetDamage();
        Instantiate(impactPrefab, transform.position, Quaternion.identity, transform);
        SoundManager.Instance.PlaySound(inpactSound, inpactVolume);
        CheckEnemyStatus();
        bullet.DestroyGameObject();
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

    public float GetCurrentValue()
    {
        return CurrentHealth;
    }

    public float GetMaxValue()
    {
        return maxHealth;
    }
}

