﻿public class InputService : MonoSingleton<InputService>
{
    private Joystick joystick;
    public Joystick Joystick{
        get{
            return joystick;
        }
    }

    public Joystick tempJoystick;

    private void Start() {
        joystick = tempJoystick;
    }
}
