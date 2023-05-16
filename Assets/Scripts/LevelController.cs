using System;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public enum LevelState
    {
        playing, 
        failed, 
        Complete
    }
    public static LevelState levelStatus
    {
        get => LevelStatus;
        private set => LevelStatus = value;
    }

    [SerializeField] private PlayerController player;
    [SerializeField] private CinemachineDollyCart levelDolly;
    [SerializeField] private CinemachinePathBase path;
    [SerializeField] private SlideMenu end;
    [SerializeField] private SlideMenu gameOver;
    private static LevelState LevelStatus;

    private void Awake()
    {
        levelStatus = LevelState.playing;
        levelDolly = GetComponent<CinemachineDollyCart>();
        player = GetComponentInChildren<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!player.IsAlive)
        {
            LevelController.levelStatus = LevelController.LevelState.failed;
            levelDolly.m_Speed = 0;
            gameOver.OpenSlide();

        }
        if (!(levelDolly.m_Position >= path.MaxPos && LevelController.levelStatus == LevelController.LevelState.playing)) return;
        LevelController.levelStatus = LevelController.LevelState.Complete;
        end.OpenSlide();
    }
}
