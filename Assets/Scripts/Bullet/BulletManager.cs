
using UnityEngine;

public class BulletManager : MonoBehaviour
{

    [SerializeField] private AskForBulletChannelSO askForBulletChannel;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private Transform world;
    [SerializeField] private Bullet bullet;
    private BulletFactory factory = new BulletFactory();

    public void Awake()
    {
        askForBulletChannel.Subscribe(SpawnBullet);
    }

    public void OnDestroy()
    {
        askForBulletChannel.Unsubscribe(SpawnBullet);
    }
    public void SpawnBullet(Transform pos, string layer,BulletConfiguration bulletConfig,Quaternion rotation)
    {
      
     
        factory.CreateBullet(bullet, pos, layer, bulletConfig, world, bulletParent,rotation);
    }
}
