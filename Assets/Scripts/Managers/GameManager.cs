using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private AudioSource _audSrc;
    [SerializeField]
    private AudioClip _sucessAudClip;
    private PlayerController _playerController;
    public int BridgePassedCount { get; set; }
    public event Action BridgePassed;
    public event Action PlayerFell;
    public event Action Respawned;

    override protected void Awake()
    {
        base.Awake();
        _playerController = FindObjectOfType<PlayerController>();
        _audSrc = GetComponent<AudioSource>();
    }


    public void OnBridgePassed()
    {
        BridgePassedCount++;
        BridgePassed?.Invoke();
        _audSrc.PlayOneShot(_sucessAudClip);
    }

    public void OnPlayerFallen()
    {
        PlayerFell?.Invoke();
    }

    public void OnPlayerRespawned(){
        Respawned?.Invoke();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void Respawn()
    {
        _playerController.Respawn();
    }
}
