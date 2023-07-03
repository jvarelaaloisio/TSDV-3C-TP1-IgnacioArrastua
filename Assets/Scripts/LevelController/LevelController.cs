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
    public static LevelState levelStatus
    {
        get => LevelStatus;
        private set => LevelStatus = value;
    }

    [SerializeField] private VoidChannelSO PlayerDeadChannel;
    [SerializeField] private AudioClip inGameMusic;
    [SerializeField] private PlayerHealthSystem player;
    [SerializeField] private EnemyBaseStats enemy;
    [SerializeField] private CinemachineDollyCart levelDolly;
    [SerializeField] private CinemachinePathBase path;
    [SerializeField] private SlideMenu end;
    [SerializeField] private SlideMenu gameOver;
    private static LevelState LevelStatus;

    private void Awake()
    {
        PlayerDeadChannel.Subscribe(OnPlayerDead);
        levelStatus = LevelState.playing;
        levelDolly = GetComponent<CinemachineDollyCart>();
        player = GetComponentInChildren<PlayerHealthSystem>();
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
        PlayerDeadChannel.Unsubscribe(OnPlayerDead);
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
