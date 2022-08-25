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
    [SerializeField]
    private AudioClip _hammerSlam;
    private AudioSource _audSrc;


    override protected void Awake()
    {
        base.Awake();
        _audSrc = GetComponent<AudioSource>();
        _audSrc.clip = _hammerSlam;
        _collider = GetComponentInChildren<Collider2D>();
    }

    internal void Start()
    {
        GameManager.Instance.Respawned += OnPlayerRespanwed;
    }

    internal void Update()
    {
        if (_build) PerformBuilding();

        if (!_collider.SeenByCamera())
        {
            ReturnToPool();
        }
    }

    internal void OnDestroy()
    {
        try
        {
            GameManager.Instance.Respawned -= OnPlayerRespanwed;
        }
        catch { }
    }

    public void StartBuilding(Vector2 position, Action onComplete)
    {
        _build = true;
        _onComplete = onComplete;
        transform.position = new Vector3(position.x, position.y, -1);
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
        _audSrc.Stop();
    }

    private void PerformBuilding()
    {
        var min = 0.1f;
        var max = TrackConstructor.Instance.BuildingsDistanceRange.Max;
        var scale = transform.localScale;
        scale.y += _scaleSpeed * Time.deltaTime;
        scale.y = Mathf.Clamp(scale.y, min, max);
        transform.localScale = scale;
        if (!_audSrc.isPlaying)
        {
            _audSrc.pitch = UnityEngine.Random.Range(0.9f, 2f);
            _audSrc.Play();
        }
    }

    override public void ReturnToPool()
    {
        transform.localScale = _defaultScale;
        transform.rotation = Quaternion.Euler(_defaultRotation);
        base.ReturnToPool();
    }

    private void OnPlayerRespanwed()
    {
        ReturnToPool();
    }

}
