using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCrosshair : MonoBehaviour
{
    [SerializeField] Transform aimPartent;
    Transform initialPosition;

    private void Start()
    {
        initialPosition = transform;
    }
    private void Update()
    {
        transform.position = new Vector3(aimPartent.position.x,transform.position.y);
    }
}
