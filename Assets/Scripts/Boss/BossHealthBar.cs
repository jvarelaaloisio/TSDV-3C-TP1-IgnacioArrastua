using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
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
