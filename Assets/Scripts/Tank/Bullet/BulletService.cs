using UnityEngine;

public class BulletService : MonoSingleton<BulletService>
{
    //Should be implemented using interface i guess. As anyone can call Fire bullet by this script.
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
