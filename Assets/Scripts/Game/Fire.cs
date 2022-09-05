using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fire : MonoBehaviour
{
    [SerializeField][Range(0,1)] private float radius = 1;
    [SerializeField] private List<Bullet> bullets;

    public UnityEvent OnStartSpawn;
    public UnityEvent OnEndSpawn;
    
    

    public List<Bullet> bulletsList;
    
    public void FireShotRandomDirection(int countBullets)
    {
        OnStartSpawn?.Invoke();
        bulletsList = new List<Bullet>();

        for (int i = 0; i < countBullets; i++)
            bulletsList.Add(CreateRandomBullet(null, transform.position));

        bulletsList.ForEach(x => ThrowBullet(x.Rb, GetRandomDirection(), x.Speed));
        OnEndSpawn?.Invoke();
    }

    public void FireShotForward(int countBullets)
    {
        OnStartSpawn?.Invoke();
        bulletsList = new List<Bullet>();

        for (int i = 0; i < countBullets; i++)
            bulletsList.Add(CreateRandomBullet(null, transform.position));

        bulletsList.ForEach(x => ThrowBullet(x.Rb, Vector2.up, x.Speed));
        OnEndSpawn?.Invoke();

    }

    private void ThrowBullet(Rigidbody2D rb, Vector3 direction, float speed)
    {
        rb.AddForce(direction.normalized * speed, ForceMode2D.Impulse);
    }

    private Vector2 GetRandomDirection()
    {
        return new Vector2(Random.Range(-radius, radius), Random.Range(0f, 1f));
    }

    private Bullet Add(Transform parent)
    {
        return Instantiate(bullets[Random.Range(0, bullets.Count)], parent);
    }

    private Bullet CreateRandomBullet(Transform parent, Vector3 position)
    {
        Bullet bullet = Instantiate(bullets[Random.Range(0, bullets.Count)], parent);
        bullet.transform.position = position;
        return bullet;
    }

}
