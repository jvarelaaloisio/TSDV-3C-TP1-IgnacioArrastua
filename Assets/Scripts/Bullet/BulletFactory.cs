using UnityEngine;
using UnityEngine.Pool;

public class BulletFactory
{
    private ObjectPool<Bullet> pool;

    public void CreateBullet(Bullet bullet, Transform pos, string layerName, DirectionHandler directionHandler, Transform world, Transform bulletParent)
    {
        var inverseRotation = Quaternion.Inverse(pos.rotation);
        var newBullet = GameObject.Instantiate(bullet, pos.position, pos.rotation * inverseRotation, bulletParent);
        newBullet.gameObject.layer = LayerMask.NameToLayer(layerName);
        newBullet.DirHandler = directionHandler;
        newBullet.SetStartPosition(pos);
        newBullet.SetActiveState(true);
        newBullet.ResetBulletTimer();
        newBullet.SetWorld(world);

    }
}