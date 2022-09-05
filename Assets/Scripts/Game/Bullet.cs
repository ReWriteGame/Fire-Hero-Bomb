using System;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase][RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{ 
    [SerializeField]private LevelCounter damageConfig;
    [SerializeField]private int damage;
    [SerializeField]private float speed;
    [SerializeField]private float radius = 0.5f;
    [SerializeField]private float missingTime = 0.5f;

    public UnityEvent OnExplose;
    public UnityEvent OnMissing;

    private Collider2D collider;
    private Rigidbody2D rb;
   
    public event Action<Bullet> OnDestroy;
   
    public int Damage => damage;
    public float Speed => speed;
    public Rigidbody2D Rb => rb;


    private void Awake()
    {
        
        if (damageConfig != null)
        {
            damage = (int)damageConfig.GetCurrentValue();
        }

        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

 

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Destroyer>())
        {
            Explose();
        }
       
        if (col.gameObject.GetComponent<Block>())
        {
            Explose();
        }

        if (col.gameObject.GetComponent<Asteroid>())
        {
            Explose();
        }

        if (col.gameObject.CompareTag("Wall"))
        {
            Invoke(nameof(Missing), missingTime);
        }
    }

    private void Missing()
    {
        Explose();
        OnMissing?.Invoke();
    }


  
    private void Explose()
    {
        collider.enabled = false;
        //gameObject.SetActive(false);
        OnExplose?.Invoke();
        OnDestroy?.Invoke(this);


    }


}