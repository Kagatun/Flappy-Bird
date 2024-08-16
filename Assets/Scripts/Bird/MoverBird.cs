using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoverBird : MonoBehaviour
{
    [SerializeField] private InputDetector _inputDetector;
    [SerializeField] private Transform _transformStartPosition;
    [SerializeField] private float _tapForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    private Rigidbody2D _rigidbody;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        transform.position = _transformStartPosition.position;

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
}
