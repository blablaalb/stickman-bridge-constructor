using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FallenPlayerDetection : MonoBehaviour
{
    private BoxCollider2D _collider;
    private PlayerController _playerController;

    internal void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _collider.isTrigger = true;
        _playerController = FindObjectOfType<PlayerController>();
    }

    internal void LateUpdate()
    {
        FollowPLayer();
    }

    internal void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() is PlayerController player)
        {
            GameManager.Instance.OnPlayerFallen();
        }
    }

    private void FollowPLayer()
    {
        var position = transform.position;
        position.x = _playerController.transform.position.x;
        transform.position = position;
    }
}
