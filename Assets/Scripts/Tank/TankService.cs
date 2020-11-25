using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TankService : MonoSingleton<TankService>
{
    private Camera mainCamera;
    private float safeDistance = 30f;
    private List<EnemyController> enemyList;
    private List<Transform> spawnPositions;
    [SerializeField]
    private SpawnerObject spawnerObject;
    [SerializeField]
    private List<TankScriptableObject> enemiesToGenerate;
    [SerializeField]
    private Transform enemyParent;
    [SerializeField]
    private TankController playerTank;
    private PlayerTracker playerTracker;

    private void Awake()
    {
        enemyList = new List<EnemyController>();
        spawnPositions = new List<Transform>();
    }
    private void Start() {
        PopulateSpawnPositions();
        GenerateEnemies();
        CreatePlayer();
    }

    #region Player Generation

        private void CreatePlayer(){
            Vector3 randomSpawnPosition = GetSafeSpawnPos();
            playerTank = GameObject.Instantiate(playerTank, randomSpawnPosition, Quaternion.identity);
            InputService inputService = GetControls();
            playerTank.OnDeath += FlushTanks;
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
                isSafe = true;
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
            playerTracker = mainCamera.gameObject.GetComponent<PlayerTracker>();
            playerTracker.PlayerTransform = playerObj.GetComponent<Transform>();
            player.OnDeath += playerTracker.UnFollowPlayer;
        }

    #endregion
    
    #region Enemy Generation

        private void GenerateEnemies(){
            for(int i = 0; i < enemiesToGenerate.Count; i++){
                EnemyController createdEnemy;
                createdEnemy = CreateEnemy(enemiesToGenerate[i]);
                enemyList.Add(createdEnemy);
            }
        }

        private EnemyController CreateEnemy(TankScriptableObject tankData){
            EnemyController enemy = tankData.type;
            Vector3 randomSpawnPosition = GetRandomSpawnPos();
            enemy = GameObject.Instantiate(enemy, randomSpawnPosition, Quaternion.identity, enemyParent);
            AssignEnemyValues(enemy, tankData);
            return enemy;
        }

        private void AssignEnemyValues(EnemyController enemy, TankScriptableObject tankData){
            enemy.Health = tankData.health;
            enemy.Speed = tankData.speed;
            enemy.Thrust = tankData.thrust;
            enemy.gameObject.name = tankData.tankName;
        }

        private Vector3 GetRandomSpawnPos(){
            Transform randomSpawnTransform = spawnPositions[Random.Range(1,spawnPositions.Count)];
            Vector3 randomSpawnPos = randomSpawnTransform.position;
            return randomSpawnPos;
        }

    #endregion

    private void PopulateSpawnPositions(){
        spawnPositions.AddRange(spawnerObject.gameObject.GetComponentsInChildren<Transform>());
    }

    private void FlushTanks(){
        for(int i = 0; i < enemyList.Count; i++){
            StartCoroutine(DestroyObject(enemyList[i].gameObject));
        }
        UnSubscribeOnDeath();
    }

    private void UnSubscribeOnDeath(){
        playerTank.OnDeath -= FlushTanks;
        playerTank.OnDeath -= playerTracker.UnFollowPlayer;
    }

    IEnumerator DestroyObject(GameObject toDestroy){
        Destroy(toDestroy);
        yield return null;
    }
}
