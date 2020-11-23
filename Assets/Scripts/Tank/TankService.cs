using System.Collections.Generic;
using UnityEngine;
public class TankService : MonoSingleton<TankService>
{
    private Camera mainCamera;
    [SerializeField]
    private float safeDistance = 30f;
    [SerializeField]
    private List<EnemyController> enemyList;

    [SerializeField]
    private List<Transform> spawnPositions;
    [SerializeField]
    private SpawnerObject spawnerObject;
    [SerializeField]
    private List<TankScriptableObject> enemiesToGenerate;
    [SerializeField]
    private Transform enemyParent;
    [SerializeField]
    private TankController playerTank;

    private Joystick joystick;
    public Transform PlayerTransform{
        get{
            Transform playerTransform = playerTank.transform;
            return playerTransform;
        }
    }

    private void Start() {
        populateSpawnPositions();
        for(int i = 0; i < enemiesToGenerate.Count; i++){
            EnemyController createdEnemy;
            createdEnemy = CreateEnemy(enemiesToGenerate[i]);
            enemyList.Add(createdEnemy);
        }
        CreatePlayer();
    }

    #region Player Generation

        private void CreatePlayer(){
            Vector3 randomSpawnPosition = GetSafeSpawnPos();
            playerTank = GameObject.Instantiate(playerTank, randomSpawnPosition, Quaternion.identity);
            InputService inputService = GetControls();
            AssignControls(playerTank, inputService);
            AssignCamera(playerTank);
        }

        private Vector3 GetSafeSpawnPos(){
            bool isSafe = false;
            Vector3 randomSpawnPos = new Vector3(0f,0f,0f);
            Transform randomSpawnTransform;
            for(int i = 1; i < spawnPositions.Count; i++){
                randomSpawnTransform = spawnPositions[Random.Range(i,spawnPositions.Count-1)];
                isSafe = CheckSafetyFromEnemy(randomSpawnTransform.position);
                randomSpawnPos = randomSpawnTransform.position;
                if(isSafe){
                    break;
                } 
            }
            if(!isSafe){
                randomSpawnTransform = spawnPositions[Random.Range(1,spawnPositions.Count)];
                //player GodMode few seconds - defence Logic;
            }
            return randomSpawnPos;
        }

        private bool CheckSafetyFromEnemy(Vector3 spawnPos){
            bool isSafe = false;
            if(enemyList != null){
                for(int i = 0; i < enemyList.Count; i++){
                    Vector3 enemyPos = enemyList[i].GetComponent<Transform>().position;
                    float enemyDistance = Vector3.Distance(spawnPos, enemyPos);
                    if(enemyDistance < safeDistance){
                        isSafe = false;
                        break;
                    } else {
                        isSafe = true;
                    }
                }
            } else {
                Debug.LogError("Null reference as it skipped!!");
            }
            return isSafe;
        }

        private InputService GetControls(){
            InputService ControlInstance = InputService.Instance;
            return ControlInstance;
        }

        private void AssignControls(TankController tankController,  InputService inputService){
            tankController.Joystick = inputService.Joystick;
            tankController.FireButton = inputService.FireButton;
            tankController.FireForce = inputService.FireForce;
        }

        private void AssignCamera(TankController player){
            mainCamera = Camera.main;
            GameObject playerObj;
            playerObj = player.gameObject;
            mainCamera.gameObject.GetComponent<PlayerTracker>().PlayerTransform = playerObj.GetComponent<Transform>();
        }

    #endregion
    
    #region Enemy Generation

        private void populateSpawnPositions(){
            spawnPositions.AddRange(spawnerObject.gameObject.GetComponentsInChildren<Transform>());
        }

        private EnemyController CreateEnemy(TankScriptableObject tankData){
            EnemyController enemy = tankData.type;
            Vector3 randomSpawnPosition = GetRandomSpawnPos();
            enemy = GameObject.Instantiate(enemy, randomSpawnPosition, Quaternion.identity, enemyParent);
            enemy.Health = tankData.health;
            enemy.Speed = tankData.speed;
            enemy.Thrust = tankData.thrust;
            enemy.gameObject.name = tankData.tankName;
            return enemy;
        }

        private Vector3 GetRandomSpawnPos(){
            Transform randomSpawnTransform = spawnPositions[Random.Range(1,spawnPositions.Count)];
            Vector3 randomSpawnPos = randomSpawnTransform.position;
            return randomSpawnPos;
        }

    #endregion
}
