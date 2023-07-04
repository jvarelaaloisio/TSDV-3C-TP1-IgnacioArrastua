using System;
using UnityEngine;
/// <summary>
/// Class for the PlayerController
/// </summary>

public class PlayerHealthSystem : MonoBehaviour, IFillable
{
    [SerializeField] private int maxHealthPoints;

    [SerializeField] private float _currentHealth;
    [SerializeField] private FillUIChannelSO fillUIChannel;
    [SerializeField] private VoidChannelSO playerDeathChannelSO;
    [field: SerializeField] public bool IsAlive { get; private set; } = true;
    private static int _score = 0;
    [SerializeField] private ParticleSystem impactPrefab;
    [SerializeField] private ParticleSystem boom;
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
        CurrentHealth = maxHealthPoints;
    }

    /// <summary>
    /// Set the damage that the player received and starts the player deactivation 
    /// </summary>
    /// <param name="damage"></param>
    public void ReceiveDamage(float damage)
    {
        CurrentHealth -= damage;
        if (!(CurrentHealth < 0.0f)) 
            return;
        playerDeathChannelSO.RaiseEvent();
        IsAlive = false;
        DeactivatePlayer();
    }

    /// <summary>
    /// Deactivates player and plays the death animation
    /// </summary>
    public void DeactivatePlayer()
    {
        IsAlive = false;
        var explosion = Instantiate(boom, transform.position, transform.rotation);
        explosion.Play();
        SoundManager.Instance.PlaySound(explosionSound, explosionVolume);
        transform.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Bullet>(out var bullet)) 
            return;

        Instantiate(impactPrefab, transform.position, Quaternion.identity, transform);
        SoundManager.Instance.PlaySound(inpactSound, inpactVolume);
        ReceiveDamage(bullet.Damage);
        bullet.DestroyGameObject();
    }
    /// <summary>
    /// Gets currentHealthPoints.
    /// Used for the PlayerHealthBar.
    /// </summary>
    /// <returns></returns>
    public float GetCurrentFillValue()
    {
        return CurrentHealth;
    }
    /// <summary>
    /// Gets maxHealthPoints
    /// Used for the PlayerHealthBar
    /// </summary>
    /// <returns></returns>
    public float GetMaxFillValue()
    {
        return maxHealthPoints;
    }

}
