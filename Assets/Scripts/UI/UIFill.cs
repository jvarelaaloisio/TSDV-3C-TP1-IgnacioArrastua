using UnityEngine;
using UnityEngine.UI;

//TODO: Hacer un intermedio que sea UIController
public class UIFill : MonoBehaviour
{
    [SerializeField] private FillUIChannelSO fillUI;
    [SerializeField] private Image imageDisplay;

  
    private float sliderMaxTimer;
    private float sliderCurrentTimer;

    private void Awake()
    {
        fillUI.Subscribe(UpdateFill);
    }
    private void OnDestroy()
    {
        fillUI.Unsubscribe(UpdateFill);
    }
    private void Start()
    {
        
    }

    
    /// <summary>
    /// Updates the player HealthBar
    /// </summary>
    private void UpdateFill(IFillable fillable)
    {
        sliderMaxTimer = fillable.GetMaxValue();
        sliderCurrentTimer = fillable.GetCurrentValue();
        var currentTime = sliderCurrentTimer / sliderMaxTimer;
        currentTime = Mathf.Clamp01(currentTime);
        imageDisplay.fillAmount = currentTime;
    }

}
