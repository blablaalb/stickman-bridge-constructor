using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverScreen;

    internal void Start()
    {
        GameManager.Instance.PlayerFell += OpenGameoverScreen;
        CloseGameoverScreen();
    }

    internal void OnDestroy()
    {
        try
        {
            GameManager.Instance.PlayerFell -= OpenGameoverScreen;
        }
        catch { }
    }

    public void OpenGameoverScreen()
    {
        _gameOverScreen.SetActive(true);
    }

    public void CloseGameoverScreen()
    {
        _gameOverScreen.SetActive(false);
    }
}
