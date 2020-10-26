public class TankService : MonoSingleton<TankService>
{
    public TankView tankView;
    private TankModel tankModel;
    private TankController tankController;
    protected Joystick stick;
    private void Start() {
        
        InputService joystickInstance = InputService.Instance;
        stick = joystickInstance.Joystick;
        tankController = CreateTank(stick);
    }

    private TankController CreateTank(Joystick stick){
        tankModel = new TankModel(10,100);
        tankController = new TankController(tankModel, tankView, stick);
        return tankController;
    }
}
