using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BridgeCount : MonoBehaviour
{
    private TextMeshProUGUI _tmPro;

    internal void Awake()
    {
        _tmPro = GetComponent<TextMeshProUGUI>();
        _tmPro.SetText("0");
        GameManager.Instance.BridgePassed += OnBridgePassed;
    }


    internal void OnDestroy()
    {
        try
        {
            GameManager.Instance.BridgePassed -= OnBridgePassed;
        }
        catch { }
    }

    private void OnBridgePassed()
    {
        _tmPro.SetText(GameManager.Instance.BridgePassedCount.ToString());
    }
}
