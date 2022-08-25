using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
using System;

public class Bridge : PoolMember<BridgePool>
{
    [SerializeField]
    private float _rotationTime;
    [SerializeField]
    private float _scaleSpeed;
    private bool _build;
    private Action _onComplete;
    private Collider2D _collider;
    private Vector3 _defaultScale = new Vector3(1, 0, 1);
    private Vector3 _defaultRotation = Vector3.zero;
    private Vector3 _fullRotation = new Vector3(0, 0, -90);


    override protected void Awake()
    {
        base.Awake();
        _collider = GetComponentInChildren<Collider2D>();
    }

    internal void Update()
    {
        if (_build) PerformBuilding();

        if (!_collider.SeenByCamera())
        {
            transform.localScale = _defaultScale;
            transform.rotation = Quaternion.Euler(_defaultRotation);
            base.ReturnToPool();
        }
    }

    public void StartBuilding(Vector2 position, Action onComplete)
    {
        _onComplete = onComplete;
        transform.position = new Vector3(position.x, position.y, -1);
        _build = true;
    }

    public void StopBuilding()
    {
        _build = false;
        transform.DORotate(_fullRotation, _rotationTime).SetEase(Ease.OutBounce).OnComplete(() =>
            {
                _onComplete?.Invoke();
                _onComplete = null;
            }
        );
    }

    private void PerformBuilding()
    {
        var scale = transform.localScale;
        scale.y += _scaleSpeed * Time.deltaTime;
        transform.localScale = scale;
    }


}
