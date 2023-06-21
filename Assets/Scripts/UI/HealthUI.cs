using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for the HealthUI
/// </summary>
/// 
public interface IFillable
{
    float GetCurrentValue();
    event Action<float> OnValueChanged;
    float GetMaxValue();
}


public class HealthUI : MonoBehaviour
{
    [SerializeField] private GameObject ObjectValues;

    private Slider slider;

    private float sliderMaxTimer;
    private float sliderCurrentTimer;

    private void Awake()
    {
         ObjectValues.GetComponent<IFillable>().OnValueChanged += OnValueChange;
    }
    private void OnDestroy()
    {
         ObjectValues.GetComponent<IFillable>().OnValueChanged -= OnValueChange;     
    }
    private void Start()
    {
        slider = GetComponent<Slider>();
        sliderMaxTimer = ObjectValues.GetComponent<IFillable>().GetMaxValue();
        sliderCurrentTimer = ObjectValues.GetComponent<IFillable>().GetCurrentValue();
        var currentTime = sliderCurrentTimer / sliderMaxTimer;
        currentTime = Mathf.Clamp01(currentTime);
        slider.value = currentTime;
    }

    private void Update()
    {
        UpdateHealthBar();
    }
    /// <summary>
    /// Updates the player HealthBar
    /// </summary>
    private void UpdateHealthBar()
    {
        sliderMaxTimer = ObjectValues.GetComponent<IFillable>().GetMaxValue();
        sliderCurrentTimer = ObjectValues.GetComponent<IFillable>().GetCurrentValue();
        var currentTime = sliderCurrentTimer / sliderMaxTimer;
        currentTime = Mathf.Clamp01(currentTime);
        slider.value = currentTime;
    }
    private void OnValueChange(float newValue)
    {
        sliderCurrentTimer = newValue;
        var currentTime = sliderCurrentTimer / sliderMaxTimer;
        currentTime = Mathf.Clamp01(currentTime);
        slider.value = currentTime;
    }
}
