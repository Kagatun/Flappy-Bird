using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _counter = 0;

    public event Action <int> Counted;

    public void AddPointCounter()
    {
        _counter++;
        Counted?.Invoke(_counter);
    }
}
