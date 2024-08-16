using System;
using UnityEngine;

[RequireComponent(typeof(BirdCollisionHandler), typeof(MoverBird))]
public class Bird : MonoBehaviour, IRemovable
{
    private BirdCollisionHandler _handler;
    private MoverBird _moverBird;

    public event Action GameOver;

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

    public void Remove()
    {
        GameOver?.Invoke();
    }

    private void ProcessCollision(IRemovable interactable)
    {
        if (interactable is Enemy || interactable is Ground || interactable is BulletEnemy)
        {
            Remove();
        }
    }
}
