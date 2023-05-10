using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    void Start()
    {
      //  movementPoints = determinedMovement.transform.Cast<Transform>().ToArray();
        transform.position = movementPoints[0].position;
        hasEnded = false;
        // endLoop = Mathf.Clamp(endLoop, startLoop, movementPoints.Length - 1);
       // startLoop = Mathf.Clamp(startLoop, 0, endLoop - 1);
    }

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
    private void OnValidate()
    {

    }
    void Update()
    {
        if (!isActive || hasEnded) return;


        Vector3 localPosition = movementPoints[currentPoint].localPosition - transform.parent.localPosition;

        Vector2 vector2Position = Vector2.MoveTowards(transform.localPosition, localPosition, speed * Time.deltaTime);

        transform.localPosition =new Vector3(vector2Position.x, vector2Position.y, transform.localPosition.z);
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

    private void EndMovement()
    {
        if (currentPoint >= movementPoints.Length)
        {
            isActive = false;
            hasEnded = true;
        }
    }

    public void SetActive(bool activeStatus = true)
    {
        isActive = activeStatus;
        transform.position = movementPoints[0].position;
    }
}
