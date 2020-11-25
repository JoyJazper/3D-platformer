using UnityEngine;
using UnityEngine.UI;
public class InputService : MonoSingleton<InputService>
{
    [SerializeField]
    private Joystick joystick;
    public Joystick Joystick{
        get{
            return joystick;
        }
    }

    [SerializeField]
    private Button fireButton;
    public Button FireButton{
        get{
            return fireButton;
        }
    }

    [SerializeField]
    private Slider fireForce;
    public Slider FireForce{
        get{
            return fireForce;
        }
    }

}
