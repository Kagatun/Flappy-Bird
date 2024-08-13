using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private SpawnerEnemies _spawnerEnemies;

    private int _counter = 0;
    public event Action <int> Counted;

    private void Start()
    {
        _spawnerEnemies.Removed += OnCount;
    }

    private void OnDisable()
    {
        _spawnerEnemies.Removed -= OnCount;
    }

    private void OnCount()
    {
        _counter++;
        Counted?.Invoke(_counter);
    }

    public void Reset()
    {
        _counter = 0;
    }
}
