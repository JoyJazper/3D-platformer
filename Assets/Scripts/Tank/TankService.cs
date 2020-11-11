﻿using UnityEngine;
public class TankService : MonoSingleton<TankService>
{
    public TankView tankView;
    private Camera mainCamera;
    private TankModel tankModel;
    public TankScriptableObject[] addedPlayers;
    private TankController tankController;
    protected Joystick stick;
    private void Start() {
        GetJoystick();
        CreateTank();
        AssignCamera();
    }

    private void GetJoystick(){
        InputService joystickInstance = InputService.Instance;
        stick = joystickInstance.Joystick;
    }

    #region Tank Creator
        private void CreateTank(){
            for(int i = 0; i < 1; i++){
                tankController = CreateTankController(addedPlayers[i], stick);
            }
            
        }
        private TankController CreateTankController(TankScriptableObject objectType,  Joystick stick){
            tankModel = new TankModel(objectType.speed, objectType.thrust, objectType.health);
            tankController = new TankController(tankModel, tankView, stick);
            return tankController;
        }
    #endregion

    private void AssignCamera(){
        mainCamera = Camera.main;
        GameObject playerObj;
        playerObj = tankController.TankView.gameObject;
        mainCamera.gameObject.GetComponent<PlayerTracker>().PlayerTransform = playerObj.GetComponent<Transform>();
    }
}
