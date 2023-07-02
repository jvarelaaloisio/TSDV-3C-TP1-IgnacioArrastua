using UnityEngine;

/// <summary>
/// Class for the EnemyBaseStats
/// </summary>
public class EnemyBaseStats : MonoBehaviour, IFillable
{
    //TODO: TP2 - Syntax - Consistency in naming convention
    //TODO: TP2 - Syntax - Fix declaration order
    public bool isActive;
    [SerializeField] private FillUIChannelSO fillUIChannel;
    [SerializeField] private GameObject model;
    [SerializeField] private float timeUntilDeath = 1f;
    [SerializeField] private int scoreValue;
    [SerializeField] private float maxHealth;
    private float _currentHealth;
    [Header("Particle")]
    [SerializeField] private ParticleSystem impactPrefab;
    private ParticleSystem boom;
    [Header("Audio")]
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

        CurrentHealth -= bullet.Damage;
        Instantiate(impactPrefab, transform.position, Quaternion.identity, transform);
        SoundManager.Instance.PlaySound(inpactSound, inpactVolume);
        CheckEnemyStatus();
        bullet.DestroyGameObject();
    }
    ///// <summary>
    /// Check if enemy is Dead
    /// </summary>
    public void CheckEnemyStatus()
    {
        if (IsAlive()) 
            return;
        KillEnemy();
        PlayerHealthSystem.Score += scoreValue;

        if (boom.isPlaying)
            return;

        boom.Play();
        Invoke(nameof(DeActivateEnemy), timeUntilDeath);
        SoundManager.Instance.PlaySound(explosionSound, explosionVolume);
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

    /// <summary>
    /// Get currentHealth points
    /// Used for the Boss HealthBar
    /// </summary>
    /// <returns></returns>

    public float GetCurrentFillValue()
    {
        return CurrentHealth;
    }
    /// <summary>
    /// Get maxHealth Points
    /// Used for the Boss HealthBar
    /// </summary>
    /// <returns></returns>
    public float GetMaxFillValue()
    {
        return maxHealth;
    }
}

