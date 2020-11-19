using UnityEngine;

public class BulletService : MonoSingleton<BulletService>
{
    [SerializeField]
    private Vector3 bulletPosOffset;

    [SerializeField]
    private Quaternion bulletDirOffset;
    public BulletController FireBullet(BulletScriptableObject bulletType, Transform attacker){
        BulletController bullet;
        bullet = bulletType.Bullet;
        Vector3 bulletPosition = attacker.position + bulletPosOffset;
        Quaternion bulletDirection = Quaternion.LookRotation(transform.forward, transform.up);
        bulletDirection = bulletDirection * bulletDirOffset;
        GameObject.Instantiate(bullet, bulletPosition, bulletDirection);
        return new BulletController();
    }


}
