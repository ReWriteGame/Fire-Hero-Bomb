using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Fire fire;
    [SerializeField] private Vector2Int countBullets;

    public event Action<Asteroid> OnExplosion;
    
    private Collider2D collider;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Bullet>())
        {
            fire.FireShotRandomDirection(Random.Range(countBullets.x, countBullets.y));
            Explosion();
        }
        
    }

    private void Explosion()
    {
        collider.enabled = false;
        rb.velocity = Vector3.zero;
        OnExplosion?.Invoke(this);
        Destroy();
    }
    
    private void Destroy()
    {
        Destroy(gameObject);
    }

}
