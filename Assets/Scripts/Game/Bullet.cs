using System;
using UnityEngine;

[SelectionBase][RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
   [SerializeField]private int damage;
   [SerializeField]private float speed;


   private Collider2D collider;
   private Rigidbody2D rb;
   
   public event Action<Bullet> OnDestroy;
   
   public int Damage => damage;
   public float Speed => speed;
   public Rigidbody2D Rb => rb;


   private void Awake()
   {
       collider = GetComponent<Collider2D>();
       rb = GetComponent<Rigidbody2D>();
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
   }

   private void Destroy()
   {
       collider.enabled = false;
       gameObject.SetActive(false);
       Destroy(gameObject);
   }
}
