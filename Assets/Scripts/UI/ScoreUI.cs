using TMPro;
using UnityEngine;
/// <summary>
/// Class for the ScoreUI
/// </summary>

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textComponent;
    [SerializeField] private string scoreText;
    [SerializeField] private IntChannelSO scoreChannelSO;
    [SerializeField] private int scoreValue;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        scoreValue = 0;
        scoreChannelSO.Subscribe(OnScoreUp);
    }

    private void OnDisable()
    {
        scoreChannelSO.Unsubscribe(OnScoreUp);
    }
    /// <summary>
    /// Changes the ScoreValue and Text
    /// </summary>
    /// <param name="obj"></param>
    private void OnScoreUp(int obj)
    {
        scoreValue += obj;
        textComponent.text = scoreText + scoreValue;
    }
}