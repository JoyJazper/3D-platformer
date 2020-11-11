using UnityEngine;

[CreateAssetMenu(fileName = "TankScriptableObject", menuName = "ScriptableObjects/NewTankScriptable")]
public class TankScriptableObject : ScriptableObject
{
    public EnemyController view;
    public TankType type;
    public string TankName;
    public float speed;
    public float thrust;
    public int health;
}
