using System.Collections.Generic;
using UnityEngine;
public class TankService : MonoSingleton<TankService>
{
    private Camera mainCamera;

    [SerializeField]
    private List<EnemyController> enemyList;
    [SerializeField]
    private List<Transform> spawnPositions;
    [SerializeField]
    private SpawnerObject spawnerObject;
    public List<TankScriptableObject> enemiesToGenerate;
    public Transform enemyParent;
    public EnemyController redTank;
    public EnemyController blueTank;
    public EnemyController greenTank;

    [SerializeField]
    private TankController playerTank;

    public Transform PlayerTransform{
        get{
            Transform playerTransform = playerTank.transform;
            return playerTransform;
        }
    }

    private void Start() {
        populateSpawnPositions();
        CreatePlayer();
        EnemyController createdEnemy;
        createdEnemy = CreateEnemy(enemiesToGenerate[0]);
        enemyList.Add(createdEnemy);
        createdEnemy = CreateEnemy(enemiesToGenerate[1]);
        enemyList.Add(createdEnemy);
        createdEnemy = CreateEnemy(enemiesToGenerate[2]);
        enemyList.Add(createdEnemy);
    }

    // Player Creation...
    private void CreatePlayer(){
        Vector3 randomSpawnPosition = GetRandomSpawnPos();
        playerTank.transform.position = randomSpawnPosition;
        Joystick joystick = GetJoystick();
        AssignJoystick(playerTank, joystick);
        AssignCamera(playerTank);
    }

    private Joystick GetJoystick(){
        InputService joystickInstance = InputService.Instance;
        Joystick tempJoystick;
        tempJoystick = joystickInstance.Joystick;
        return tempJoystick;
    }

    private void AssignJoystick(TankController tankController,  Joystick joystick){
        tankController.Joystick = joystick;
    }

    private void AssignCamera(TankController player){
        mainCamera = Camera.main;
        GameObject playerObj;
        playerObj = player.gameObject;
        mainCamera.gameObject.GetComponent<PlayerTracker>().PlayerTransform = playerObj.GetComponent<Transform>();
    }

    // Enemy Creation... Enemy tank factory pattern and controlled by EnemyService

    private void populateSpawnPositions(){
        spawnPositions.AddRange(spawnerObject.gameObject.GetComponentsInChildren<Transform>());
    }

    private EnemyController CreateEnemy(TankScriptableObject tankData){
        TankType type = tankData.type;
        EnemyController enemy;
        Vector3 randomSpawnPosition = GetRandomSpawnPos();
        if(type == TankType.Blue){
            enemy = GameObject.Instantiate(blueTank, randomSpawnPosition, Quaternion.identity, enemyParent);
        } else if(type == TankType.Red){
            enemy = GameObject.Instantiate(redTank, randomSpawnPosition, Quaternion.identity, enemyParent);
        } else {
            enemy = GameObject.Instantiate(greenTank, randomSpawnPosition, Quaternion.identity, enemyParent);
        }
        enemy.Health = tankData.health;
        enemy.Speed = tankData.speed;
        enemy.Thrust = tankData.thrust;
        enemy.gameObject.name = tankData.TankName;
        return enemy;
    }

    private Vector3 GetRandomSpawnPos(){
        Transform randomSpawnTransform = spawnPositions[Random.Range(1,spawnPositions.Count)];
        Vector3 randomSpawnPos = randomSpawnTransform.position;
        return randomSpawnPos;
    }
}
