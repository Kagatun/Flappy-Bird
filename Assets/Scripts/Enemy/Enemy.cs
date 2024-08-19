using System;
using UnityEngine;

public class Enemy : MonoBehaviour , IRemovable
{
    [SerializeField] private AttackerEnemy _attackerEnemy;

    public event Action<Enemy> Spawned;

    public void InitializationSpawnerBullet(SpawnerBulletEnemy spawnerBulletEnemy)
    {
        _attackerEnemy.Initialization(spawnerBulletEnemy);
    }

    public void Remove()
    {
        Spawned?.Invoke(this);
    }
}
