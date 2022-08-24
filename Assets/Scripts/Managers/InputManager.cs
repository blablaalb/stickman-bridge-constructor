using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

public class InputManager : Singleton<InputManager>
{
    public event Action MouseDown;
    public event Action MouseUp;


    internal void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseDown?.Invoke();
        }

        if (Input.GetMouseButtonUp(0))
        {
            MouseUp?.Invoke();
        }
    }

}
