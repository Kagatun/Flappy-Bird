using System.Collections;
using UnityEngine;

public class AttackerEnemy : MonoBehaviour
{
    private SpawnerBulletEnemy _spawnerBulletEnemy;
    private WaitForSecondsRealtime _wait;

    private float _offsetX = -1.3f;
    private float _offsetY = 0.5f;
    private float _minRandom = 0.5f;
    private float _maxRandom = 2.5f;
    private float _randomTimeGeneration;

    private void Awake()
    {
        _randomTimeGeneration = Random.Range(_minRandom, _maxRandom + 1);
        _wait = new WaitForSecondsRealtime(_randomTimeGeneration);
    }

    public void StartShooting()
    {
        StartCoroutine(Shooting());
    }

    public void Initialization(SpawnerBulletEnemy spawnerBulletEnemy)
    {
        _spawnerBulletEnemy = spawnerBulletEnemy;
    }

    private IEnumerator Shooting()
    {
        while (enabled)
        {
            _randomTimeGeneration = Random.Range(_minRandom, _maxRandom + 1);

            yield return _wait;

            SpawnBullet();
        }
    }

    private void SpawnBullet()
    {
        Vector3 correctivePosition = new Vector3(_offsetX, _offsetY, 0);
        _spawnerBulletEnemy.Spawn(transform.position + correctivePosition, transform.right);
    }
}
