using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int BridgePassedCount { get; set; }
    public event Action BridgePassed;
    public event Action PlayerFell;


    public void OnBridgePassed()
    {
        BridgePassedCount++;
        BridgePassed?.Invoke();
    }

    public void OnPlayerFallen()
    {
        PlayerFell?.Invoke();
    }   

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
