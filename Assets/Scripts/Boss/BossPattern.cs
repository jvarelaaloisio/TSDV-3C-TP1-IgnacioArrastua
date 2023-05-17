using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BossPattern : MonoBehaviour
{



    private bool isActive = false;
    [SerializeField] private Color color;
    [SerializeField] public Transform[] points;
    [SerializeField] public float speed;
    [SerializeField] public bool shouldLoop = false;
    [SerializeField] public float loopTimes;
    [SerializeField] public int startLoop;
    [SerializeField] public int endLoop;

    private void OnValidate()
    {
        points = transform.Cast<Transform>().ToArray();

    }

    void Start()
    {
        if (isActive)
            StartPattern();
    }

    public void StartPattern()
    {

        endLoop = Mathf.Clamp(endLoop, startLoop, points.Length - 1);
        startLoop = Mathf.Clamp(startLoop, 0, endLoop - 1);
        isActive = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        for (int i = 0; i < points.Length; i++)
        {
            if (i == points.Length - 1) return;
            Transform t = points[i];
            Transform next = points[i + 1];
            Gizmos.DrawLine(t.position, next.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(color.r, color.g, color.b, 0.1f);
        for (int i = 0; i < points.Length; i++)
        {
            if (i == points.Length - 1) return;
            Transform t = points[i];
            Transform next = points[i + 1];
            Gizmos.DrawLine(t.position, next.position);
        }
    }
}