
using System;
using TMPro;
using UnityEngine;


public class ScoreUI : MonoBehaviour
{
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

    private void OnScoreUp(int obj)
    {
        scoreValue = obj;
        Debug.Log(scoreValue);
        textComponent.text = scoreText + scoreValue;
    }
}