using System.Collections;
using UnityEngine;

public class SpawnerEnemies : SpawnerObjects<Enemy>
{
    [SerializeField] private SpawnerBulletEnemy _spawnerBulletEnemy;

    private float _minSpawnPositionY = -3.85f;
    private float _maxSpawnPositionY = 3.85f;
    private float _minRandomTime = 0.7f;
    private float _maxRandomTime = 2.2f;
    private float _randomTimeGeneration;

    public void OnEnable()
    {
        StartCoroutine(GenerateEnemy());
    }

    protected override Enemy CreateObject()
    {
        Enemy enemy = Instantiate(prefab);

        return enemy;
    }

    protected override void OnGet(Enemy enemy)
    {
        enemy.Spawned += RemoveObject;
        base.OnGet(enemy);
    }

    protected override void OnRelease(Enemy enemy)
    {
        enemy.Spawned -= RemoveObject;
        base.OnRelease(enemy);
    }

    protected override void Destroy(Enemy enemy)
    {
        enemy.Spawned -= RemoveObject;
        base.Destroy(enemy);
    }

    private Enemy Spawn()
    {
        float spawnPositionY = Random.Range(_minSpawnPositionY, _maxSpawnPositionY);
        Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);

        Enemy enemy = pool.Get();
        enemy.InitializationSpawnerBullet(_spawnerBulletEnemy);
        enemy.transform.position = spawnPoint;

        return enemy;
    }

    private IEnumerator GenerateEnemy()
    {
        while (enabled)
        {
            _randomTimeGeneration = Random.Range(_minRandomTime, _maxRandomTime);

            yield return new WaitForSecondsRealtime(_randomTimeGeneration);

            Spawn();
        }
    }
}
