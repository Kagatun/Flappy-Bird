using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BirdCollisionHandler), typeof(MoverBird))]
public class Bird : MonoBehaviour, IRemoveble
{
    private BirdCollisionHandler _handler;
    private MoverBird _moverBird;

    public event Action GameOver;

    public void Reset()
    {
        _moverBird.Reset();
    }

    private void Awake()
    {
        _handler = GetComponent<BirdCollisionHandler>();
        _moverBird = GetComponent<MoverBird>();
    }

    private void OnEnable()
    {
        _handler.CollisionDetected += ProcessCollision;
    }

    private void OnDisable()
    {
        _handler.CollisionDetected -= ProcessCollision;
    }

    private void ProcessCollision(IRemoveble interactable)
    {
        if (interactable is Enemy || interactable is Ground || interactable is BulletEnemy)
        {
            Destroy();
        }
    }

    public void Destroy()
    {
        GameOver?.Invoke();
    }

    public void Remove()
    {
        Destroy();
    }
}
