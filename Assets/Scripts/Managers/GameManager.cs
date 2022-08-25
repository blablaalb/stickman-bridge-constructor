using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;

public class GameManager : Singleton<GameManager>
{
    public int BridgePassedCount {get; set;}
    public event Action BridgePassed;


    public void OnBridgePassed()
    {
        BridgePassedCount++;
        BridgePassed?.Invoke();
    }
}
