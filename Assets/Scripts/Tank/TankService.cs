public class TankService : MonoSingleton<TankService>
{
    public TankView tankView;
    private TankModel tankModel;
    private TankController tankController;
    private void Start() {
        tankController = CreateTank();
    }

    private TankController CreateTank(){
        tankModel = new TankModel(10,100);
        tankController = new TankController(tankModel,tankView);
        return tankController;
    }
}
