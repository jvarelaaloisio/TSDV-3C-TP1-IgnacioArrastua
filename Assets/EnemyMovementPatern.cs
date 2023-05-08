using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovementPatern : MonoBehaviour
{

    [SerializeField] Transform[] points;
    private void OnValidate()
    {
        points = transform.Cast<Transform>().ToArray();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < points.Length; i++)
        {
            if (i == points.Length - 1) return;
            Transform t = points[i];
            Transform next = points[i+1];
            Gizmos.DrawLine(t.position, next.position);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.1f);
        for (int i = 0; i < points.Length; i++)
        {
            if (i == points.Length - 1) return;
            Transform t = points[i];
            Transform next = points[i+1];
            Gizmos.DrawLine(t.position, next.position);
        }
    }
}
