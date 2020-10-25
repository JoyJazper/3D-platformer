using UnityEngine;
public class TankController
{
    public TankController(TankModel initTankModel, TankView initTankView){
        tankModel = initTankModel;
        tankView = GameObject.Instantiate<TankView>(initTankView);
    }

    private TankModel tankModel;
    public TankModel TankModel{
        get{
            return tankModel;
        }
    }

    private TankView tankView;
    public TankView TankView{
        get{
            return tankView;
        }
    }

}
