using System;
using UnityEngine;

public class InputDetector : MonoBehaviour
{
    public event Action FlyUp;
    public event Action Fired;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FlyUp?.Invoke();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Fired?.Invoke();
        }
    }
}
