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

//TODO: Hacer un intermedio que sea UIController
public class UIFill : MonoBehaviour
{
    [SerializeField] private MonoBehaviour ValueToDisplay;
    [SerializeField] private Image imageDisplay;

  
    private float sliderMaxTimer;
    private float sliderCurrentTimer;

    private void Awake()
    {
         ValueToDisplay.GetComponent<IFillable>().OnValueChanged += OnValueChange;
    }
    private void OnDisable()
    {
         ValueToDisplay.GetComponent<IFillable>().OnValueChanged -= OnValueChange;     
    }
    private void Start()
    {
       
        sliderMaxTimer = ValueToDisplay.GetComponent<IFillable>().GetMaxValue();
        sliderCurrentTimer = ValueToDisplay.GetComponent<IFillable>().GetCurrentValue();
        var currentTime = sliderCurrentTimer / sliderMaxTimer;
        currentTime = Mathf.Clamp01(currentTime);
        imageDisplay.fillAmount = currentTime;
    }

    
    /// <summary>
    /// Updates the player HealthBar
    /// </summary>
    private void UpdateFill()
    {
        sliderMaxTimer = ValueToDisplay.GetComponent<IFillable>().GetMaxValue();
        sliderCurrentTimer = ValueToDisplay.GetComponent<IFillable>().GetCurrentValue();
        var currentTime = sliderCurrentTimer / sliderMaxTimer;
        currentTime = Mathf.Clamp01(currentTime);
        imageDisplay.fillAmount = currentTime;
    }
    private void OnValueChange(float newValue)
    {
        sliderCurrentTimer = newValue;
        var currentTime = sliderCurrentTimer / sliderMaxTimer;
        currentTime = Mathf.Clamp01(currentTime);
        imageDisplay.fillAmount = currentTime;
    }
}
