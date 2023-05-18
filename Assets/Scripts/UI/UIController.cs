using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class for the UIController
/// </summary>


public class UIController : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private CanvasGroup InGameUi;
    [SerializeField] private CanvasGroup pauseUI;
    void Start()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

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
            pauseUI.GetComponent<SlideMenu>().OpenSlide();
            Invoke(nameof(ChangeTimeScale), 0.7f);
        }
        else
        {
            ChangeTimeScale();
            pauseUI.GetComponent<SlideMenu>().ReturnToGame();
            Invoke(nameof(UpdateCanvas), 0.7f);

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
            pauseUI.GetComponent<SlideMenu>().OpenSlide();
            Invoke(nameof(ChangeTimeScale), 0.7f);
        }
        else
        {
            ChangeTimeScale();
            pauseUI.GetComponent<SlideMenu>().ReturnToGame();
            Invoke(nameof(UpdateCanvas), 0.7f);

        }

    }

    /// <summary>
    /// Change the timeScale according to isPaused
    /// </summary>
    public void ChangeTimeScale()
    {
        Time.timeScale = isPaused ? 0 : 1;
    }
    /// <summary>
    /// Updates the pauseUi and InGameUi according to isPaused
    /// </summary>
    public void UpdateCanvas()
    {
        pauseUI.alpha = isPaused ? 1 : 0;
        pauseUI.blocksRaycasts = isPaused;
        pauseUI.interactable = isPaused;

        InGameUi.alpha = !isPaused ? 1 : 0;
        InGameUi.blocksRaycasts = !isPaused;
        InGameUi.interactable = !isPaused;
    }
    /// <summary>
    /// Set the InGameUi and PauseUI to not be visible or interactable
    /// </summary>
    public void EndScreenSecuence()
    {
        InGameUi.alpha = 0;
        InGameUi.blocksRaycasts = false;
        InGameUi.interactable = false;
        isPaused = false;
        pauseUI.alpha = 0;
        pauseUI.blocksRaycasts = false;
        pauseUI.interactable = false;
    }

}
