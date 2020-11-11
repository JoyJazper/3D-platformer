public class TankModel
{
    public TankModel(TankScriptableObject objectType){
        speed = objectType.speed/10;
        thrust = objectType.thrust/100;
        health = objectType.health;
    }

    public TankModel(float initSpeed, float initThrust, int initHealth){
        speed = initSpeed/10;
        thrust = initThrust/100;
        health = initHealth;
    }

    private TankType tankType;
    public TankType TankType{
        get{
            return tankType;
        }
    }

    private float speed;
    public float Speed {
        get{
            return speed;
        }
    }

    private float thrust;
    public float Thrust {
        get{
            return thrust;
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
