using UnityEngine;

public class EnemyBaseStats : MonoBehaviour
{
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

    void Start()
    {
        isActive = true;
        boom = GetComponentInChildren<ParticleSystem>();
        boom.enableEmission = false;
        bc = GetComponent<BoxCollider>();
        CurrentHealth = maxHealth;
        boom.Stop();

    }



    public void StartObject()
    {
        model.SetActive(true);
        CurrentHealth = maxHealth;
        isActive = true;

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "PlayerBullet" && isActive)
        {
            other.gameObject.GetComponent<Bullet>().ResetBulletTimer();
            other.gameObject.GetComponent<Bullet>().SetStartPosition(Vector3.zero);
            other.gameObject.GetComponent<Bullet>().SetActiveState(false);
            CurrentHealth -= other.gameObject.GetComponent<Bullet>().GetDamage();
            Instantiate(impactPrefab, transform.position, Quaternion.identity, this.transform);
            SoundManager.Instance.PlaySound(inpactSound, inpactVolume);
            CheckEnemyStatus();
        }
    }
    public void CheckEnemyStatus()
    {
        if (CurrentHealth <= 0)
        {
            KillEnemy();
            PlayerController.Score += scoreValue;
            if (boom.isPlaying == false)
            {
                boom.Play();
                Invoke(nameof(DeActivateEnemy), 1f);
                SoundManager.Instance.PlaySound(explosionSound, explosionVolume);
            }
        }
    }

    public void DeActivateEnemy()
    {
        transform.gameObject.SetActive(false);
    }
    public bool IsAlive()
    {
        return (CurrentHealth > 0);
    }
    void KillEnemy()
    {
        model.SetActive(false);
        isActive = false;
    }
}

