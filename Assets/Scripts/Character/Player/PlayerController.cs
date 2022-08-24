using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    private CharacterAnimation _characterAnimation;
    private Hammer _hammer;
    private BridgePool _bridgePool;
    private Bridge _bridge;
    private Building _building;
    private bool _waitForBridge;

    internal void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
        _characterAnimation = GetComponentInChildren<CharacterAnimation>();
        _hammer = GetComponentInChildren<Hammer>();
        _bridgePool = FindObjectOfType<BridgePool>();
    }

    internal void Start()
    {
        InputManager.Instance.MouseDown += OnMouseDown;
        InputManager.Instance.MouseUp += OnMouseUp;

        Run();
    }

    internal void OnDestroy()
    {
        try
        {
            InputManager.Instance.MouseDown -= OnMouseDown;
            InputManager.Instance.MouseUp -= OnMouseUp;
        }
        catch { }
    }

    public void Run()
    {
        _characterMovement.Run();
        _characterAnimation.Run();
    }

    public void Stop()
    {
        _characterMovement.Stop();
        _characterAnimation.Idle();
    }

    public void OnBuildingEndReached(Building building)
    {
        Stop();
        _hammer.PlayAnimation();
        _waitForBridge = true;
        _building = building;
    }

    private void OnMouseDown()
    {
        if (_waitForBridge)
        {
            _bridge = _bridgePool.Get();
            _bridge.StartBuilding(_building.BuildingEnd.transform.position, Run);
        }
    }

    private void OnMouseUp()
    {
        if (_waitForBridge && _bridge)
        {
            _waitForBridge = false;
            _bridge.StopBuilding();
            _bridge = null;
            _hammer.Hide();
        }
    }


}
