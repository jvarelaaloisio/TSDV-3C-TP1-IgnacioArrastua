using UnityEngine;

/// <summary>
/// Class for the BossMovement
/// </summary>
public class BossMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 initialLocalPos;
    [SerializeField] private float minDistance;
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private BossPattern[] patterns;
    [SerializeField] private int currentPoint = 0;
    [SerializeField] private bool shouldLoop = false;
    [SerializeField] private float loopTimes;
    [SerializeField] private int startLoop;
    [SerializeField] private int endLoop;
    [SerializeField] private bool isActive = false;
    [SerializeField] private bool hasEnded = false;
    [SerializeField] private float currentLoopTimes;
    private bool isAlive;
    private EnemyBaseStats _enemyBaseStats;

    private void Awake()
    {
        _enemyBaseStats = GetComponent<EnemyBaseStats>();
    }

    private void Start()
    {
        initialLocalPos = transform.localPosition;
        if (movementPoints.Length != 0)
        {
            transform.position = movementPoints[0].position;
        }

        isActive = false;
        hasEnded = false;
        isAlive = true;
    }
    /// <summary>
    /// Set the parameters to replicate the pattern
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="shouldLoop"></param>
    /// <param name="loopTimes"></param>
    /// <param name="startLoop"></param>
    /// <param name="endLoop"></param>
    /// <param name="isActive"></param>
    /// <param name="determinedMovement"></param>
    public void SetStartParameters(float speed, bool shouldLoop, float loopTimes, int startLoop, int endLoop, bool isActive, Transform[] determinedMovement)
    {
        this.speed = speed;
        this.shouldLoop = shouldLoop;
        this.loopTimes = loopTimes;
        this.startLoop = startLoop;
        this.endLoop = endLoop;
        this.isActive = isActive;
        this.movementPoints = determinedMovement;
        this.hasEnded = false;
    }

    void Update()
    {

        isAlive = _enemyBaseStats.IsAlive();
        if (isActive & !hasEnded & isAlive)

        {


            Vector3 localPosition = movementPoints[currentPoint].localPosition - transform.parent.localPosition;

            Vector2 vector2Position = Vector2.MoveTowards(transform.localPosition, localPosition, speed * Time.deltaTime);

            transform.localPosition = new Vector3(vector2Position.x, vector2Position.y, transform.localPosition.z + localPosition.z);
            if (!(Vector2.Distance(transform.localPosition, localPosition) < minDistance)) return;
            if (!shouldLoop)
            {
                currentPoint++;
                EndMovement();
            }
            else
            {
                if (currentPoint == endLoop && currentLoopTimes < loopTimes)
                {
                    currentPoint = startLoop;
                    currentLoopTimes++;
                }
                else
                {
                    currentPoint++;
                    EndMovement();

                }
            }
        }
        else if (hasEnded)
        {
            var localPosition = initialLocalPos - transform.parent.localPosition;

            var vector2Position = Vector2.MoveTowards(transform.localPosition, localPosition, speed * Time.deltaTime);
            transform.localPosition = new Vector3(vector2Position.x, vector2Position.y, transform.localPosition.z + localPosition.z);
        }
    }
    /// <summary>
    /// Sets the hasEnded variable when the enemy reaches his 
    /// </summary>
    private void EndMovement()
    {
        if (currentPoint >= movementPoints.Length)
        {
            isActive = false;
            hasEnded = true;
        }
    }
    /// <summary>
    /// Returns hasEnded
    /// </summary>
    /// <returns></returns>
    public bool hasMovementEnded()
    {
        return hasEnded;
    }
    /// <summary>
    /// Sets a new Pattern randomly from an Array
    /// </summary>
    public void StartNewPattern()
    {
        var pattern = patterns[Random.Range(0, patterns.Length)];
        SetStartParameters(pattern.speed, pattern.shouldLoop, pattern.loopTimes, pattern.startLoop, pattern.endLoop, true, pattern.points);
    }

    /// <summary>
    /// Set the Movement Active and moves to the default position
    /// </summary>
    /// <param name="activeStatus">Sets the player ActiveState</param>
    public void SetActive(bool activeStatus = true)
    {
        isActive = activeStatus;
        transform.position = movementPoints[0].position;
    }
}
