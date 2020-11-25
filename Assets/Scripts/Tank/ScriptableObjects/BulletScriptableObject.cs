using UnityEngine;
[CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObjects/NewBulletScriptable")]
public class BulletScriptableObject : ScriptableObject
{
    public int Power;
    public BulletController Bullet;
}
