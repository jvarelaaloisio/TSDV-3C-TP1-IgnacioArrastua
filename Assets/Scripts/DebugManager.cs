using UnityEngine;

/// <summary>
/// Class for the DebugManager
/// </summary>
public class DebugManager : MonoBehaviour
{
    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)
    //TODO: TP2 - Syntax - Fix declaration order
    PlayerMovement player;
    public static DebugManager _instance;
    public static DebugManager Instance
    {
        get => _instance;
        set => _instance = value;
    }
    [SerializeField] private bool isPlayerActive;
    [SerializeField] private bool isCameraActive;
    [SerializeField] private bool isDebugActive;
    [SerializeField] private bool isLogActive;
    [SerializeField] private bool isLogWarningActive;
    [SerializeField] private bool isLogErrorActive;
    [SerializeField] private bool isRayActive;
    [SerializeField] private string[] activeTags;

    //TODO: TP2 - Syntax - Consistency in access modifiers (private/protected/public/etc)
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// LogWarning method controller
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="text"></param>
    public void LogWarning(string tag, string text)
    {
        if (!isDebugActive) return;
        if (!isLogWarningActive) return;
        //TODO: TP2 - SOLID
        switch (tag)
        {
            case "Player":
                if (isPlayerActive)
                {
                    Debug.LogWarning(text);
                }
                break;
            case "MainCamera":
                if (isCameraActive)
                {
                    Debug.LogWarning(text);
                }
                break;
            default:
                Debug.LogError("Invalid Tag");
                break;
        }
    }
    /// <summary>
    /// LogError method controller
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="text"></param>
    public void LogError(string tag, string text)
    {

        if (!isDebugActive) return;
        if (!isLogErrorActive) return;
        //TODO: TP2 - SOLID
        switch (tag)
        {
            case "Player":
                if (isPlayerActive)
                {
                    Debug.LogError(text);
                }
                break;
            case "MainCamera":
                if (isCameraActive)
                {
                    Debug.LogError(text);
                }
                break;
            default:
                Debug.LogError("Invalid Tag");
                break;
        }
    }
    /// <summary>
    /// Log method controller
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="text"></param>
    public void Log(string tag, string text)
    {
        if (!isDebugActive) return;
        if (!isLogActive) return;     
        //TODO: TP2 - SOLID
        switch (tag)
        {
            case "Player":
                if (isPlayerActive)
                {
                    Debug.Log(text);
                }
                break;
            case "MainCamera":
                if (isCameraActive)
                {
                    Debug.Log(text);
                }
                break;
            default:
                Debug.LogError("Invalid Tag");
                break;
        }
    }

}
