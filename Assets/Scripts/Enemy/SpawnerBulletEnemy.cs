using UnityEngine;

public class SpawnerBulletEnemy : SpawnerObjects<BulletEnemy>
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _bulletSpeed;

    public BulletEnemy Spawn(Vector3 position, Vector2 direction)
    {
        BulletEnemy bullet = pool.Get();
        bullet.transform.position = position;
        bullet.SetDirectionSpeed(direction, _bulletSpeed);

        return bullet;
    }

    protected override BulletEnemy CreateObject()
    {
        BulletEnemy bullet = Instantiate(prefab);
        bullet.SetLayerMaskTarget(_layerMask);

        return bullet;
    }

    protected override void OnGet(BulletEnemy bullet)
    {
        bullet.Fired += OnFired;
        base.OnGet(bullet);
    }

    protected override void OnRelease(BulletEnemy bullet)
    {
        bullet.Fired -= OnFired;
        base.OnRelease(bullet);
    }

    protected override void Destroy(BulletEnemy bullet)
    {
        bullet.Fired -= OnFired;
        base.Destroy(bullet);
    }

    private void OnFired(Bullet bullet)
    {
        RemoveObject((BulletEnemy)bullet);
    }
}
