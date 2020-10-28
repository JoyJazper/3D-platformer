public class TankModel
{
    public TankModel(float initSpeed, float initThrust, int initHealth){
        speed = initSpeed/10;
        thrust = initThrust/100;
        health = initHealth;
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
