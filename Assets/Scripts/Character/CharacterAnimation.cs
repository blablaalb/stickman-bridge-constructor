using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator _animator;

    internal void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Run()
    {
        _animator.Play("Run");
    }

    public void Idle()
    {
        _animator.Play("Idle");
    }
}
