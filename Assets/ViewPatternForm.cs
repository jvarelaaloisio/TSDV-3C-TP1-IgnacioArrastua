using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPatternForm : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private Transform[] points;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDrawGizmos()
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (i == points.Length - 1) return;
           Gizmos.DrawLine(points[i].position, points[i+1].position);
        }
    }
}
