using UnityEngine;

public class BulletService : MonoSingleton<BulletService>
{
    public void FireBullet(BulletScriptableObject bulletType, Transform bulletHandle, float distance){
        BulletController bullet;
        bullet = bulletType.Bullet;
        Vector3 bulletPosition = bulletHandle.position;
        bullet = GameObject.Instantiate(bullet, bulletPosition, bulletHandle.rotation);
        bullet.ForceAngle = distance;
        bullet.BulletOwner = bulletHandle;
        bullet.AttackPower = bulletType.Power;
        bullet.Fire();
    }
}
