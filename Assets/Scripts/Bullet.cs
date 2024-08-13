using System;
using System.Collections;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    private LayerMask _targetMask;
    private WaitForSecondsRealtime _wait;
    private Vector2 _direction;
    private float _flightTime = 4f;
    private float _speed;

    public event Action<Bullet> Fired;

    private void Awake()
    {
        _wait = new WaitForSecondsRealtime(_flightTime);
    }

    private void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if ((_targetMask.value & 1 << collider.gameObject.layer) != 0)
        {
            if (collider.TryGetComponent(out IRemoveble removeble))
            {
                removeble.Remove();
            }

            Destroy();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(SelfDestruct());
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

    public void Destroy()
    {
        Fired?.Invoke(this);
    }

    private IEnumerator SelfDestruct()
    {
        yield return _wait;

        Destroy();
    }
}
