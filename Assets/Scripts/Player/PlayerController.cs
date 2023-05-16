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
    [SerializeField] private ParticleSystem boom;

    public static int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScoreUp?.Invoke(_score);
        }
    }

    private void Awake()
    {
      
    }

    void Start()
    {
        currentHealth = maxHealthPoints;
        bc = GetComponent<BoxCollider>();
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

            var explosion =Instantiate(boom,transform.position, transform.rotation);
            explosion.Play();
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
