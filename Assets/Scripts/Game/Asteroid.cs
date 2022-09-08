using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Fire fire;
    [SerializeField] private Vector2Int countBullets;
    [SerializeField] private LevelCounter bullets;
    [SerializeField] private Vector3 size;
    [SerializeField] private SkinView skinView;

    public UnityEvent<Asteroid> OnExplosion;
    
    private Collider2D collider;
    private Rigidbody2D rb;
    private int skinId;
    
    private void Awake()
    {
        skinId = Random.Range(0, 4);
        skinView.SetSkin(skinId);
        
        fire.OnEndSpawn.AddListener(ApplyColor);
        
        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void ApplyColor()
    {
        fire.bulletsList.ForEach(x => x.SetSkin(skinId));
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Bullet>())
        {
            fire.FireShotRandomDirection(Random.Range((int)bullets.GetCurrentValue() - 1, (int)bullets.GetCurrentValue() + 1));
            Explosion();
        }
     
        if (col.gameObject.GetComponent<BlockCollector>())
        {
            fire.FireShotRandomDirection(Random.Range((int)bullets.GetCurrentValue() - 1, (int)bullets.GetCurrentValue() + 1));
            Explosion();
        }
        
        if (col.gameObject.GetComponent<Block>())
        {
            Destroy(0);
        }
        if (col.gameObject.GetComponent<EndPoint>())
        {
            Destroy(gameObject);
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
            Destroy(0);
        }
        
        if (col.gameObject.GetComponent<Ship>())
        {
            Explosion();
        }
        
        if (col.gameObject.GetComponent<EndPoint>())
        {
            Destroy(0);
        }
        
        
    }

    private void Explosion()
    {
        rb.angularVelocity = 0;
        rb.isKinematic = true;
        collider.enabled = false;
        OnExplosion?.Invoke(this);
        Destroy(1f);
    }
    
    private void Destroy(float time)
    {
        Destroy(gameObject, time);
    }
}
