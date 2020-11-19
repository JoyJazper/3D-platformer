using UnityEngine;

[CreateAssetMenu(fileName = "TankScriptableObject", menuName = "ScriptableObjects/NewTankScriptable")]
public class TankScriptableObject : ScriptableObject
{
    public EnemyController type;
    public string tankName;
    public float speed;
    public float thrust;
    public int health;
}
