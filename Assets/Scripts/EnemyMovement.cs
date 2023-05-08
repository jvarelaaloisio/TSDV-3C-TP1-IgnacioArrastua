using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;
    [SerializeField] private float smoothTime;
    [SerializeField] public GameObject determinedMovement;
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private int currentPoint = 0;
    [SerializeField] private bool shouldLoop = false;
    [SerializeField] private float loopTimes;
    [SerializeField] private int startLoop;
    [SerializeField] private int endLoop;
    [SerializeField] private bool isActive = false;
    private Vector2 currentVelocity;
    [SerializeField] private float currentLoopTimes;

    void Start()
    {
        movementPoints = determinedMovement.transform.Cast<Transform>().ToArray();
        transform.position = movementPoints[0].position;
        endLoop = Mathf.Clamp(endLoop, startLoop, movementPoints.Length - 1);
        startLoop = Mathf.Clamp(startLoop, 0, endLoop - 1);
    }

    private void OnValidate()
    {

    }
    void Update()
    {
        if (isActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, movementPoints[currentPoint].position, speed * Time.deltaTime);
           // Vector2.SmoothDamp(transform.position, movementPoints[currentPoint].position, ref currentVelocity, smoothTime, speed, Time.deltaTime);
            if (Vector2.Distance(transform.position, movementPoints[currentPoint].position) < minDistance)
            {
                if (!shouldLoop)
                {
                    currentPoint++;
                    if (currentPoint >= movementPoints.Length)
                    {
                        currentPoint = 0;
                        transform.position = movementPoints[0].position;
                    }
                }
                else
                {
                    if (currentPoint == endLoop && currentLoopTimes < loopTimes)
                    {
                        currentPoint = startLoop;
                        currentLoopTimes++;
                       transform.position += movementPoints[currentPoint].position;
                    }
                    else
                    {
                        currentPoint++;
                        if (currentPoint >= movementPoints.Length)
                        {
                            currentPoint = 0;
                            transform.position = movementPoints[0].position;
                        }
                    }
                }
            }
        }
    }
}
