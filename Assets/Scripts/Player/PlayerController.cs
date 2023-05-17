using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static event Action<int> OnScoreUp;

    [SerializeField] private int maxHealthPoints;
    private float currentHealth;
    private BoxCollider bc;
    [SerializeField] private bool isDamageable = true;
    [field: SerializeField] public bool IsAlive { get; private set; } = true;
    private static int _score = 0;
    [SerializeField] private ParticleSystem impactPrefab;
    [SerializeField] private ParticleSystem boom;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] [Range(0, 1)] private float explosionVolume;
    [SerializeField] private AudioClip inpactSound;
    [SerializeField] [Range(0, 1)] private float inpactVolume;


    public static int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScoreUp?.Invoke(_score);
            SoundManager.Instance.PlaySoundScore();
        }
    }

    private void Awake()
    {
        bc = GetComponent<BoxCollider>();

    }

    void Start()
    {
        currentHealth = maxHealthPoints;
        PlayerMovement.OnRoll += PlayerMovement_OnRoll;
        isDamageable = true;
        Score = 0;
    }


    private void OnDestroy()
    {
        PlayerMovement.OnRoll -= PlayerMovement_OnRoll;
    }
    private void PlayerMovement_OnRoll(bool isOnRoll)
    {
        isDamageable = !isOnRoll;
    }
    public void ReceiveDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth < 0.0f)
        {
            IsAlive = false;

            var explosion = Instantiate(boom, transform.position, transform.rotation);
            explosion.Play();
            SoundManager.Instance.PlaySound(explosionSound, explosionVolume);
            DeactivatePlayer();

        }
    }

    public void DeactivatePlayer()
    {
        transform.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyBullet") && isDamageable)
        {
            Debug.Log("EnemyBulletHit");
            other.gameObject.GetComponent<Bullet>().ResetBulletTimer();
            other.gameObject.GetComponent<Bullet>().SetStartPosition(Vector3.zero);
            other.gameObject.GetComponent<Bullet>().SetActiveState(false);
            Instantiate(impactPrefab, transform.position, Quaternion.identity, transform);
            SoundManager.Instance.PlaySound(inpactSound, inpactVolume);
            ReceiveDamage(other.gameObject.GetComponent<Bullet>().GetDamage());
        }
        if (other.gameObject.CompareTag("BossBullet") && isDamageable)
        {
          
            other.gameObject.GetComponent<Bullet>().ResetBulletTimer();
            other.gameObject.GetComponent<Bullet>().SetStartPosition(Vector3.zero);
            other.gameObject.GetComponent<Bullet>().SetActiveState(false);
            Instantiate(impactPrefab, transform.position, Quaternion.identity, transform);
            SoundManager.Instance.PlaySound(inpactSound, inpactVolume);
            ReceiveDamage(other.gameObject.GetComponent<Bullet>().GetDamage());
        }
        if (other.gameObject.CompareTag("BossRay") && isDamageable)
        {
            
            Instantiate(impactPrefab, transform.position, Quaternion.identity, transform);
            SoundManager.Instance.PlaySound(inpactSound, inpactVolume);
            ReceiveDamage(currentHealth*0.87f);
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
