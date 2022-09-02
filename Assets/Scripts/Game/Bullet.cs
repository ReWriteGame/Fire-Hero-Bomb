using System;
using UnityEngine;

[SelectionBase][RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{ 
    [SerializeField]private LevelCounter damageConfig;
    [SerializeField]private int damage;
    [SerializeField]private float speed;
    [SerializeField]private float radius = 0.5f;
    //[SerializeField]private ContactFilter2D filter;
    [SerializeField]private int numMaskLayer;
    private int layer;


    private Collider2D collider;
    private Rigidbody2D rb;
   
    public event Action<Bullet> OnDestroy;
   
    public int Damage => damage;
    public float Speed => speed;
    public Rigidbody2D Rb => rb;


    private void Awake()
    {
        layer = 1 << numMaskLayer;
        
        if (damageConfig != null)
        {
            damage = (int)damageConfig.GetCurrentValue();
        }

        collider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

 

    private void CheckCollider()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, radius);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Destroyer>())
        {
            OnDestroy?.Invoke(this);
            Destroy();
        }
       
        if (col.gameObject.GetComponent<Block>())
        {
            OnDestroy?.Invoke(this);
            Destroy();
        }

        //Collider2D[] arr = new Collider2D[10];
        //col.GetContacts(arr);
        ReflectProjectile();
    }

    private Collider2D[] colliders;
    private RaycastHit2D hit2;
    private Vector2 currentMovementDirection;
    //private void FixedUpdate()
    //{
    //    currentMovementDirection = -rb.velocity;
    //    //Debug.Log(currentMovementDirection);
////
    //    hit2 = Physics2D.Raycast(transform.position, currentMovementDirection, layer);
    //    ReflectProjectile();
    //}
    
    public void ReflectProjectile()
    {
        currentMovementDirection = rb.velocity;
        hit2 = Physics2D.Raycast(transform.position, currentMovementDirection, layer);
        if (hit2.collider != null)
        {
            rb.velocity = Vector2.Reflect(currentMovementDirection, hit2.normal);
            //Debug.Log(hit2.normal);
        }
//
        //Collider2D hit3 = Physics2D.OverlapCircle(transform.position, radius);
//
        //if (hit3 != null)
        //{
        //    rb.velocity = Vector2.Reflect(currentMovementDirection, hit2.normal);
        //    Debug.Log(hit2.normal);
        //}
        
       //RaycastHit hit;
       //Ray ray = new Ray(transform.position, currentMovementDirection);

       //if (Physics.Raycast(ray, out hit))
       //{
       //    rb.velocity = Vector3.Reflect(currentMovementDirection, hit.normal);
       //    Debug.Log(3232);
       //}
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        //col.
    }

    private void Rebound(Collision2D col, Vector3 velocity)
    {
        float speed = velocity.magnitude;
        Vector3 direction = Vector3.Reflect(velocity.normalized, col.contacts[0].normal);
        rb.velocity = direction * speed;
    }

    private void Destroy()
    {
        collider.enabled = false;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}