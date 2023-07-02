using UnityEngine;
using UnityEngine.InputSystem;using UnityEngine.InputSystem.XInput;

//TODO: TP2 - Syntax - Fix formatting
/// <summary>
/// Class for the UIController
/// </summary>
public class UIController : MonoBehaviour
{
    //TODO: TP2 - Syntax - Consistency in naming convention
    [SerializeField] private CanvasGroup inGameUiCanvas;
    [SerializeField] private CanvasGroup pauseUiCanvas;
    [SerializeField] private float timeUntilTheCanvasIsUpdated = 0.7f;
    private bool isPaused = false;
  
    private void Start()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    //TODO - Fix - Using Input related logic outside of an input responsible class
    /// <summary>
    /// Toggle Pause depending on Input
    /// </summary>
    /// <param name="ctx">Input</param>

    public void SetPause(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        isPaused = !isPaused;
        if (isPaused)
        {
            UpdateCanvas();
            pauseUiCanvas.GetComponent<SlideMenu>().OpenSlide();
            ToggleTimeScaleOnPauseState();
        }
        else
        {
            ToggleTimeScaleOnPauseState();
            pauseUiCanvas.GetComponent<SlideMenu>().ReturnToGame();
            Invoke(nameof(UpdateCanvas), timeUntilTheCanvasIsUpdated);
        }

    }
    /// <summary>
    /// Toggle Pause depending on Input
    /// </summary>
    public void SetPause()
    {
       
        isPaused = !isPaused;
        if (isPaused)
        {
            UpdateCanvas();
            pauseUiCanvas.GetComponent<SlideMenu>().OpenSlide();
            ToggleTimeScaleOnPauseState();
        }
        else
        {
            ToggleTimeScaleOnPauseState();
            pauseUiCanvas.GetComponent<SlideMenu>().ReturnToGame();
            Invoke(nameof(UpdateCanvas), timeUntilTheCanvasIsUpdated);

        }

    }
    /// <summary>
    /// Change the timeScale according to isPaused
    /// </summary>
    public void ToggleTimeScaleOnPauseState()
    {
        Time.timeScale = isPaused ? 0 : 1;
    }
    /// <summary>
    /// Updates the pauseUi and InGameUi according to isPaused
    /// </summary>
    public void UpdateCanvas()
    {
        pauseUiCanvas.alpha = isPaused ? 1 : 0;
        pauseUiCanvas.blocksRaycasts = isPaused;
        pauseUiCanvas.interactable = isPaused;

        inGameUiCanvas.alpha = !isPaused ? 1 : 0;
        inGameUiCanvas.blocksRaycasts = !isPaused;
        inGameUiCanvas.interactable = !isPaused;
    }
    /// <summary>
    /// Set the InGameUi and PauseUI to not be visible or interactable
    /// </summary>
    public void EndScreenSecuence()
    {
        inGameUiCanvas.alpha = 0;
        inGameUiCanvas.blocksRaycasts = false;
        inGameUiCanvas.interactable = false;
        isPaused = false;
        pauseUiCanvas.alpha = 0;
        pauseUiCanvas.blocksRaycasts = false;
        pauseUiCanvas.interactable = false;
    }

}