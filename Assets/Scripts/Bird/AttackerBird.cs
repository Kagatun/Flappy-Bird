using UnityEngine;

public class AttackerBird : MonoBehaviour
{
    [SerializeField] private SpawnerBulletBird _spawnerBulletBird;
    [SerializeField] private InputDetector _inputDetector;

    private float _offsetX = 1.3f;
    private float _offsetY = 0.5f;

    private void OnEnable()
    {
        _inputDetector.Fired += Shoot;
    }

    private void OnDisable()
    {
        _inputDetector.Fired -= Shoot;
    }

    private void Shoot()
    {
        Vector3 correctivePosition = new Vector3(_offsetX, _offsetY, 0);
        _spawnerBulletBird.Spawn(transform.position + correctivePosition, transform.right);
    }
}
