using UnityEngine;

public class SpawnerBulletBird : SpawnerObjects<BulletBird>
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private ScoreCounter _scoreCounter;

    public BulletBird Spawn(Vector3 position, Vector2 direction)
    {
        BulletBird bullet = pool.Get();
        bullet.transform.position = position;
        bullet.SetDirectionSpeed(direction, _bulletSpeed);

        return bullet;
    }

    protected override BulletBird CreateObject()
    {
        BulletBird bullet = Instantiate(prefab);
        bullet.SetLayerMaskTarget(_layerMask);

        return bullet;
    }

    protected override void OnGet(BulletBird bullet)
    {
        bullet.Fitted += AddPoint;
        bullet.Fired += OnFired;
        base.OnGet(bullet);
    }

    protected override void OnRelease(BulletBird bullet)
    {
        bullet.Fitted -= AddPoint;
        bullet.Fired -= OnFired;
        base.OnRelease(bullet);
    }

    protected override void Destroy(BulletBird bullet)
    {
        bullet.Fitted -= AddPoint;
        bullet.Fired -= OnFired;
        base.Destroy(bullet);
    }

    private void OnFired(Bullet bullet)
    {
        RemoveObject((BulletBird)bullet);
    }

    private void AddPoint()
    {
        _scoreCounter.AddPointCounter();
    }
}
