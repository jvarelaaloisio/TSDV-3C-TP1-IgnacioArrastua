using UnityEngine;
using UnityEngine.Pool;

public class BulletFactory
{
    private ObjectPool<Bullet> pool;

    public void CreateBullet(Bullet bullet, Transform pos, string layerName, DirectionHandler directionHandler, Transform world, Transform bulletParent)
    {
       var newBullet = GameObject.Instantiate(bullet, pos.position, Quaternion.identity, bulletParent);
       newBullet.gameObject.layer = LayerMask.NameToLayer(layerName);
       newBullet.DirHandler = directionHandler;
       newBullet.SetWorld(world);
    }
}