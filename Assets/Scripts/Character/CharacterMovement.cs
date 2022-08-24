using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField]
    private float _speed;
    private bool _run;

    internal void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    internal void Update()
    {
        if (_run)
            PerformRun();
    }

    public void Run()
    {
        _run = true;
    }

    public void Stop()
    {
        _run = false;
        var velocity = _rb.velocity;
        velocity.x = 0;
        _rb.velocity = velocity;
    }

    private void PerformRun()
    {
        var velocity = _rb.velocity;
        velocity.x = _speed;
        _rb.velocity = velocity;
    }

}
