using UnityEngine;
using UnityEngine.UI;


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

    /// <summary>
    /// Updates the player HealthBar
    /// </summary>
    private void UpdateFill(IFillable fillable)
    {
        sliderMaxTimer = fillable.GetMaxFillValue();
        sliderCurrentTimer = fillable.GetCurrentFillValue();
        var currentTime = sliderCurrentTimer / sliderMaxTimer;
        currentTime = Mathf.Clamp01(currentTime);
        imageDisplay.fillAmount = currentTime;


    }

}
