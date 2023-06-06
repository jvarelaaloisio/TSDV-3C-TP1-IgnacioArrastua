using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for the BossHealthBar
/// </summary>
public class BossHealthBar : MonoBehaviour
{
  
    //TODO: TP2 - Syntax - Consistency in naming convention
    [SerializeField]
    private EnemyBaseStats bossHealthBar;

    private Slider slider;

    private float sliderMaxTimer;
    private float sliderCurrentTimer;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        sliderMaxTimer = bossHealthBar.GetMaxHealthPoints();
        sliderCurrentTimer = bossHealthBar.GetCurrentHealthPoints();
        var currentTime = sliderCurrentTimer / sliderMaxTimer;
        currentTime = Mathf.Clamp01(currentTime);
        slider.value = currentTime;
    }
}
