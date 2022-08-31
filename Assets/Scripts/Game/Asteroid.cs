using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Fire fire;
    [SerializeField] private Vector2Int countBullets;
    [SerializeField] private LevelCounter bullets;

    public event Action<Asteroid> OnExplosion;
    
    private Collider2D collider;
    
    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Bullet>())
        {
            fire.FireShotRandomDirection(Random.Range((int)bullets.GetCurrentValue() - 1, (int)bullets.GetCurrentValue() + 1));
            Explosion();
        }
     
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Destroyer>())
        {
            Destroy(gameObject);
        }
        
        if (col.gameObject.GetComponent<Block>())
        {
            Destroy();
        }
    }

    private void Explosion()
    {
        collider.enabled = false;
        OnExplosion?.Invoke(this);
        Destroy();
    }
    
    private void Destroy()
    {
        Destroy(gameObject);
    }
}
