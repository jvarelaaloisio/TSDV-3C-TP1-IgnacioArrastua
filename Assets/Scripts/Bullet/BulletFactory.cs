using UnityEngine;

public class BulletFactory
{
    public void CreateBullet(Bullet bullet, Transform pos, string layerName, BulletConfiguration bulletConfig, Transform world, Transform bulletParent,Quaternion rotation)
    {
        var newBullet = GameObject.Instantiate(bullet, pos.position,  rotation, bulletParent);
        newBullet.gameObject.layer = LayerMask.NameToLayer(layerName);
        newBullet.DirHandler = bulletConfig.directionHandler;
        newBullet.Damage = bulletConfig.damage;
        newBullet.Velocity = bulletConfig.velocity;
        newBullet.SetStartPosition(pos);
        newBullet.SetWorld(world);

    }
}