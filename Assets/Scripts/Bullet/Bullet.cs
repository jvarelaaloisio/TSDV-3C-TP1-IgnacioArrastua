using System.Collections;
using UnityEngine;
/// <summary>
/// Class for the BulletClass
/// </summary>
public class Bullet : MonoBehaviour
{
    public float Velocity { get; set; } = 50f;
    public float Damage { get; set; } = 30f;
    public static float maxAliveTime = 3f;
    public DirectionHandler DirHandler { get; set; }
    private Transform world;
    private Vector3 direction;

    private IEnumerator Start()
    {
        Destroy(gameObject, maxAliveTime);
        while (gameObject.activeSelf)
        {
            direction = DirHandler.GetDirection(transform, world);
            transform.localPosition += Time.deltaTime * Velocity * direction;
            yield return null;
        }
    }


    /// <summary>
    /// Set the World of the bullet
    /// </summary>
    /// <param name="worldTransform">World to use calculations</param>
    public void SetWorld(Transform worldTransform)
    {
        world = worldTransform;
        direction = world.transform.InverseTransformDirection(transform.forward);
    }

    /// <summary>
    /// Set bullet spawnPosition
    /// </summary>
    /// <param name="spawnPosition">Spawn position of the bullet</param>
    public void SetStartPosition(Transform spawnPosition)
    {
        transform.position = spawnPosition.position;
    }

    /// <summary>
    /// Destroy the GameObject attached to the bullet
    /// </summary>
    public void DestroyGameObject()
    {
        Destroy(this.gameObject);
    }


}
