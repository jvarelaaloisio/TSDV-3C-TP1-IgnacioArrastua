using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "BulletConfig")]
public class BulletConfiguration : ScriptableObject
{
    public float velocity;
    public float damage;
    public DirectionHandler directionHandler;
}