using UnityEngine;
public class TankService : MonoSingleton<TankService>
{
    public TankView tankView;
    private Camera mainCamera;
    private TankModel tankModel;
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
            tankController = CreateTankController(stick);
        }
        private TankController CreateTankController(Joystick stick){
            tankModel = new TankModel(10f, 30f, 100);
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
