using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    private LayerMask _targetMask;
    private Vector2 _direction;
    private float _speed;

    public event Action<Bullet> Fired;
    public event Action Fitted;

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((_targetMask.value & 1 << collider.gameObject.layer) != 0)
        {
            if (collider.TryGetComponent(out IRemovable removable))
            {
                removable.Remove();
                Fitted?.Invoke();
            }

            Remove();
        }
    }

    public void SetLayerMaskTarget(LayerMask layerMask)
    {
        _targetMask = layerMask;
    }

    public void SetDirectionSpeed(Vector2 direction, float speed)
    {
        _speed = speed;
        _direction = direction.normalized;

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void Remove()
    {
        Fired?.Invoke(this);
    }
}
