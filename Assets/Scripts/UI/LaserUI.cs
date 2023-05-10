using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserUI : MonoBehaviour
{
    [SerializeField]
    private PlayerShooting playerShooting;

    private Slider slider;

    private float sliderMaxTimer;
    private float sliderCurrentTimer;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    private void Update()
    {
        sliderMaxTimer = playerShooting.GetSpecialBeanCooldown();
        sliderCurrentTimer = playerShooting.GetSpecialBeanCooldownTimer();
        var currentTime = sliderCurrentTimer / sliderMaxTimer;
        currentTime = Mathf.Clamp01(currentTime);
        slider.value = currentTime;
    }
}
