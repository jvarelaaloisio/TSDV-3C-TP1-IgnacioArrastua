using UnityEngine;

public class BulletManager : MonoBehaviour
{

    [SerializeField] private Transform bulletParent;
    [SerializeField] private Transform world;
    [SerializeField] private Bullet bullet;
    private BulletFactory factory;


    public void SpawnBullet(Transform pos, string layer, DirectionHandler directionHandler)
    {
        factory.CreateBullet(bullet,pos,layer,directionHandler,world,bulletParent);
    }
}