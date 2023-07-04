using UnityEngine;

/// <summary>
/// Class for the EnemyMovement
/// </summary>
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private int currentPoint = 0;
    [SerializeField] private bool shouldLoop = false;
    [SerializeField] private float loopTimes;
    [SerializeField] private int startLoop;
    [SerializeField] private int endLoop;
    [SerializeField] private bool isActive = false;
    [SerializeField] private bool hasEnded = false;
    [SerializeField] private float currentLoopTimes;
    private bool isAlive;
    private EnemyBaseStats enemyBaseStats;

    private void Awake()
    {
        enemyBaseStats = GetComponent<EnemyBaseStats>();
    }

    private void Start()
    {
        if (movementPoints.Length != 0)
        {
            transform.position = movementPoints[0].position;
        }
        isActive = false;
        hasEnded = false;
        isAlive = true;
    }
    /// <summary>
    /// Set the parameters of the enemy to follow a new movement
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
        transform.position = movementPoints[0].position;
    }

    private void Update()
    {

        isAlive = enemyBaseStats.IsAlive();
        if (!isActive || hasEnded || !isAlive) return;

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

    /// <summary>
    /// Sets the hasEnded variable when the enemy reaches his destined location
    /// </summary>
    private void EndMovement()
    {
        if (currentPoint < movementPoints.Length) 
            return;
        isActive = false;
        hasEnded = true;
        enemyBaseStats.DeactivateEnemy();
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
