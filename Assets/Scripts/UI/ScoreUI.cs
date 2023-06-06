using TMPro;
using UnityEngine;
/// <summary>
/// Class for the ScoreUI
/// </summary>

public class ScoreUI : MonoBehaviour
{
    //TODO: TP2 - Syntax - Fix formatting
   [SerializeField] private TMP_Text textComponent;
    [SerializeField] private string scoreText;
    [SerializeField] private int scoreValue;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        scoreValue = 0;
        PlayerController.OnScoreUp += OnScoreUp;
    }

    private void OnDestroy()
    {
        PlayerController.OnScoreUp -= OnScoreUp;
    }
    /// <summary>
    /// Changes the ScoreValue and Text
    /// </summary>
    /// <param name="obj"></param>
    private void OnScoreUp(int obj)
    {
        scoreValue = obj;
        //TODO - Fix - Bad log/Log out of context
        Debug.Log(scoreValue);
        textComponent.text = scoreText + scoreValue;
    }
}