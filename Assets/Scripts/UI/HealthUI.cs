using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerHealthStats;

    private Slider slider;

    private float sliderMaxTimer;
    private float sliderCurrentTimer;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        sliderMaxTimer = playerHealthStats.GetMaxHealthPoints();
        sliderCurrentTimer = playerHealthStats.GetCurrentHealthPoints();
        var currentTime = sliderCurrentTimer / sliderMaxTimer;
        currentTime = Mathf.Clamp01(currentTime);
        slider.value = currentTime;
    }
}
