using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float speed = 1;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.forward * Time.deltaTime * speed;
    }
}
