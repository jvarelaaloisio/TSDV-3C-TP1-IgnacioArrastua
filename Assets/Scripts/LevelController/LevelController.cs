using Cinemachine;
using UnityEngine;

/// <summary>
/// Class for the LevelController
/// </summary>
public class LevelController : MonoBehaviour
{
    //TODO: TP2 - Syntax - Fix declaration order
    //TODO: TP2 - FSM
    /// <summary>
    /// Enum for the level state
    /// </summary>
    public enum LevelState
    {
        playing,
        failed,
        Complete
    }
    public static LevelState levelStatus { get; private set; }

    [SerializeField] private VoidChannelSO playerDeadChannel;
    [SerializeField] private VoidChannelSO onBossDeath;
    [SerializeField] private IntChannelSO scoreChannelSO;
    [SerializeField] private AudioClip inGameMusic;
    [SerializeField] private CinemachineDollyCart levelDolly;
    [SerializeField] private CinemachinePathBase path;
    [SerializeField] private SlideMenu end;
    [SerializeField] private SlideMenu gameOver;
    public static int score  =0;

    private void Awake()
    {
        playerDeadChannel.Subscribe(OnPlayerDead);
        scoreChannelSO.Subscribe(OnScoreUp);
        onBossDeath.Subscribe(OnLevelCompleted);
        levelStatus = LevelState.playing;
        levelDolly = GetComponent<CinemachineDollyCart>();
        score  =0;
    }
    private void Start()
    {
        SoundManager.Instance.GetMusicSource().Stop();
        var soundManager = SoundManager.Instance.GetMusicSource();
        soundManager.clip = inGameMusic;
        soundManager.Play();
    }
    private void OnDisable()
    {
        playerDeadChannel.Unsubscribe(OnPlayerDead);
        scoreChannelSO.Unsubscribe(OnScoreUp);
        onBossDeath.Unsubscribe(OnLevelCompleted);
    }

    private static void OnScoreUp(int obj)
    {
        SoundManager.Instance.PlaySoundScore();
        score += obj;
    }

    private void Update()
    {
        LevelCompletionLogic();
    }
    //TODO: TP2 - Syntax - Consistency in naming convention
    /// <summary>
    /// Checks if the Level should end because of loseTime or condition
    /// </summary>
    private void LevelCompletionLogic()
    {
        if (!(levelDolly.m_Position >= path.MaxPos &&
              LevelController.levelStatus == LevelController.LevelState.playing)) return;
        OnLevelCompleted();
    }
    private void OnPlayerDead()
    {
        LevelController.levelStatus = LevelController.LevelState.failed;
        levelDolly.m_Speed = 0;
        gameOver.OpenSlide();
        enabled = false;
    }
    private void OnLevelCompleted()
    {
        LevelController.levelStatus = LevelController.LevelState.Complete;
        end.OpenSlide();
        enabled = false;
    }

}
