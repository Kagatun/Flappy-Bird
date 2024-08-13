using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoverBird : MonoBehaviour
{
    [SerializeField] private InputDetector _inputDetector;
    [SerializeField] private float _tapForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    private Vector3 _startPosition;
    private Rigidbody2D _rigidbody;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    private void Start()
    {
        float startPositionX = -8.47f;
        float startPositionY = 3.91f;
        _startPosition = new Vector3(startPositionX, startPositionY);
        transform.position = _startPosition;

        _rigidbody = GetComponent<Rigidbody2D>();

        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);

        _inputDetector.FlyUp += TakeOff;
    }

    private void Update()
    {
        Fall();
    }

    private void OnDisable()
    {
        _inputDetector.FlyUp -= TakeOff;
    }

    private void TakeOff()
    {
        _rigidbody.velocity = new Vector2(_speed, _tapForce);
        transform.rotation = _maxRotation;
    }

    private void Fall()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }

    public void Reset()
    {
        _rigidbody.velocity = Vector2.zero;
        transform.position = _startPosition;
    }
}
