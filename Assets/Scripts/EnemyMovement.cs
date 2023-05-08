using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float minDistance;
    [SerializeField] public GameObject determinedMovement;
    [SerializeField] private Transform[] movementPoints;
    [SerializeField] private int currentPoint = 0;
    [SerializeField] private bool isActive = false;

    void Start()
    {
        movementPoints = determinedMovement.transform.Cast<Transform>().ToArray();
        transform.position = movementPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, movementPoints[currentPoint].position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, movementPoints[currentPoint].position) < minDistance)
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
