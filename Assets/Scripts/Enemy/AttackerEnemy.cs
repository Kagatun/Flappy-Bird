using System.Collections;
using UnityEngine;

public class AttackerEnemy : MonoBehaviour
{
    private SpawnerBulletEnemy _spawnerBulletEnemy;

    private float _offsetX = -1f;
    private float _minRandom = 0.7f;
    private float _maxRandom = 2f;
    private float _randomTimeGeneration;

    private void OnEnable()
    {
        StartCoroutine(Shooting());
    }

    public void Initialization(SpawnerBulletEnemy spawnerBulletEnemy)
    {
        _spawnerBulletEnemy = spawnerBulletEnemy;
    }

    private void SpawnBullet()
    {
        Vector3 correctivePosition = new Vector3(_offsetX, 0, 0);
        _spawnerBulletEnemy.Spawn(transform.position + correctivePosition, transform.right);
    }

    private IEnumerator Shooting()
    {
        while (Time.timeScale == 1f)
        {
            _randomTimeGeneration = Random.Range(_minRandom, _maxRandom);

            yield return new WaitForSecondsRealtime(_randomTimeGeneration);

            SpawnBullet();
        }
    }
}
