using UnityEngine;
using UnityEngine.Pool;

public abstract class SpawnerBullet<T> : MonoBehaviour where T : Bullet
{  
    [SerializeField] private T _bulletPrefab;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _bulletSpeed;

    private ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>(CreateObject, OnGet, OnRelease, Destroy, true);
    }  

    public T Spawn(Vector3 position, Vector2 direction)
    {
        T bullet = _pool.Get();
        bullet.transform.position = position;
        bullet.SetDirectionSpeed(direction, _bulletSpeed);
        return bullet;
    }

    private void ReleaseToPool(T obj)
    {
        _pool.Release(obj);
    }

    private void RemoveObject(Bullet obj)
    {
        ReleaseToPool((T)obj);
    }

    private T CreateObject()
    {
        T bullet = Instantiate(_bulletPrefab);
        bullet.Fired += RemoveObject;
        bullet.SetLayerMaskTarget(_layerMask);
        return bullet;
    }

    private void OnGet(T bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void OnRelease(T bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void Destroy(T bullet)
    {
        bullet.Fired -= RemoveObject;
        Destroy(bullet.gameObject);
    }
}
