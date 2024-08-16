using UnityEngine;
using UnityEngine.Pool;

public abstract class SpawnerObjects<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T prefab;

    protected ObjectPool<T> pool;

    private void Awake()
    {
        pool = new ObjectPool<T>(CreateObject, OnGet, OnRelease, Destroy, true);
    }

    protected void ReleaseToPool(T obj)
    {
        pool.Release(obj);
    }

    protected void RemoveObject(T obj)
    {
        ReleaseToPool(obj);
    }

    protected virtual T CreateObject()
    {
        T bullet = Instantiate(prefab);

        return bullet;
    }

    protected virtual void OnGet(T bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    protected virtual void OnRelease(T bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    protected virtual void Destroy(T bullet)
    {
        Destroy(bullet.gameObject);
    }
}
