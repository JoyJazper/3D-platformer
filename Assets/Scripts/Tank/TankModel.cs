public class TankModel
{
    public TankModel(int initSpeed, int initHealth){
        speed = initSpeed;
        health = initHealth;

    }

    private int speed = 10;
    public int Speed {
        get{
            return speed;
        }
    }
    private bool active = true;
    public bool Active {
        get {
            return active;
        }

        set {
            active = value;
        }
    }

    private int health = 100;

    public int Health{
        get{
            return health;
        }
        set{
            health = value;
        }
    }

}
