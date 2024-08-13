using System;
using UnityEngine;

public class Enemy : MonoBehaviour , IRemoveble
{
    [SerializeField] private AttackerEnemy _attackerEnemy;

    public event Action<Enemy> Spawned;

    public void Construct(SpawnerBulletEnemy spawnerBulletEnemy)
    {
        _attackerEnemy.Initialization(spawnerBulletEnemy);
        _attackerEnemy.StartShooting();
    }

    public void Remove()
    {
        Spawned?.Invoke(this);
    }
}
