using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.tvOS;

public class SpawnerEnemies : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private SpawnerBulletEnemy _spawnerBulletEnemy;

    private ObjectPool<Enemy> _pool;
    private List<Enemy> _enemyList;
    private WaitForSecondsRealtime _wait;
    private Coroutine _coroutine;

    private float _minSpawnPositionY = -2.5f;
    private float _maxSpawnPositionY = 2.5f;
    private float _minRandomTime = 1f;
    private float _maxRandomTime = 2.5f;
    private float _randomTimeGeneration;

    public event Action Removed;

    private void Awake()
    {
        _enemyList = new List<Enemy>();
        _coroutine = StartCoroutine(GenerateEnemy());
        _randomTimeGeneration = UnityEngine.Random.Range(_minRandomTime, _maxRandomTime + 1);
        _wait = new WaitForSecondsRealtime(_randomTimeGeneration);
        _pool = new ObjectPool<Enemy>(CreateObject, OnGet, OnRelease, Destroy, true);
    }

    public void StartSpawnEnemy()
    {
        StartCoroutine(GenerateEnemy());
    }

    public void Reset()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        foreach (Enemy enemy in _enemyList)
        {
            ReleaseToPool(enemy);
        }

        _enemyList.Clear();
    }

    private Enemy Spawn()
    {
        float spawnPositionY = UnityEngine.Random.Range(_minSpawnPositionY, _maxSpawnPositionY + 1);
        Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);

        Enemy enemy = _pool.Get();
        enemy.Construct(_spawnerBulletEnemy);
        enemy.transform.position = spawnPoint;
        _enemyList.Add(enemy);

        return enemy;
    }

    private void ReleaseToPool(Enemy enemy)
    {
        _pool.Release(enemy);
    }

    private void RemoveObject(Enemy enemy)
    {
        ReleaseToPool(enemy);
    }

    private Enemy CreateObject()
    {
        Enemy enemy = Instantiate(_enemyPrefab);
        enemy.Spawned += RemoveObject;

        return enemy;
    }

    private void OnGet(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void OnRelease(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        Removed?.Invoke();
    }

    private void Destroy(Enemy enemy)
    {       
        enemy.Spawned -= RemoveObject;
        Destroy(enemy.gameObject);
    }

    private IEnumerator GenerateEnemy()
    {
        while (enabled)
        {
            _randomTimeGeneration = UnityEngine.Random.Range(_minRandomTime, _maxRandomTime + 1);

            yield return _wait;

            Spawn();
        }
    }
}
