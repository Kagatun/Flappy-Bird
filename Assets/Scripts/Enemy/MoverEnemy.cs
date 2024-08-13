using UnityEngine;

public class MoverEnemy : MonoBehaviour
{
    private float _speed = -1.3f;

    private void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
}
