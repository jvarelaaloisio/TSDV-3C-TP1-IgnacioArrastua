using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private static bool isPaused = false;
   [SerializeField] private CanvasGroup InGameUi;
   [SerializeField] private CanvasGroup PauseUI;
    
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = isPaused ? 0 : 1;
    }

    public static void SetPause()
    {
        isPaused = !isPaused;
    }
}
