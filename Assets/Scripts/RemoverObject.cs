using UnityEngine;

public class RemoverObject : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet))
        {
            bullet.Remove();
        }

        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemy.Remove();
        }
    }
}
