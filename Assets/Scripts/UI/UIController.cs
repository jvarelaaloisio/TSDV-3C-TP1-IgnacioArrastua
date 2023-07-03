using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class for the UIController
/// </summary>
public class UIController : MonoBehaviour
{
    [Header("Channels")]
    [SerializeField] private VoidChannelSO OnPauseChannel;
    [Header("Canvas")]
    [SerializeField] private CanvasGroup inGameUiCanvas;
    [SerializeField] private CanvasGroup pauseUiCanvas;
    [SerializeField] private float timeUntilTheCanvasIsUpdated = 0.7f;
    private bool isPaused = false;

    private void Awake()
    {
        OnPauseChannel.Subscribe(SetPause);
    }

    private void OnDestroy()
    {
        OnPauseChannel.Unsubscribe(SetPause);
    }

    private void Start()
    {
        Time.timeScale = 1;
        isPaused = false;
    }

    /// <summary>
    /// Toggle Pause logic.
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