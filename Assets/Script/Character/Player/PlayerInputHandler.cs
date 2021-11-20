using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : Singleton<PlayerInputHandler>
{
    public float GetXMovementInputByAndroid(Joystick joystick)
    {
        return joystick.Horizontal;
    }
    public float GetYMovementInputByAndroid(Joystick joystick)
    {
        return joystick.Vertical;
    }
    public float GetXMovementInputByPC()
    {
        return Input.GetAxisRaw("Horizontal");
    }

}
