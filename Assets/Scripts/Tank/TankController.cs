using UnityEngine;
public class TankController
{
    public TankController(TankModel initTankModel, TankView initTankView){
        tankModel = initTankModel;
        tankView = GameObject.Instantiate<TankView>(initTankView);
    }



    private void Update() {
        InputToMove();
        tankView.updateMovement(horizontalMove, verticalMove);
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


    #region Player Movement Input

        float horizontalMove;
        float verticalMove;

        //Input update
        public void InputToMove(){
            horizontalMove = Input.GetAxisRaw("Horizontal")*tankModel.Speed;
            verticalMove = Input.GetAxisRaw("Vertical")*tankModel.Speed;
        }


    #endregion


}
