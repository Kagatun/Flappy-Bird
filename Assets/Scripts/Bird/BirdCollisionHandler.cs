using System;
using UnityEngine;

[RequireComponent(typeof(Bird))]
public class BirdCollisionHandler : MonoBehaviour
{
    public event Action<IRemovable> CollisionDetected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IRemovable interactable))
        {
            CollisionDetected?.Invoke(interactable);
        }
    }  
}
