using System;
using UnityEngine;

[SelectionBase][RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
   [SerializeField]private int damage;

   private Collider2D collider;

   public int Damage => damage;

   public event Action<Bullet> OnDestroy;


   private void Awake()
   {
       collider = GetComponent<Collider2D>();
   }

   private void OnCollisionEnter2D(Collision2D col)
   {
       if (col.gameObject.GetComponent<Destroyer>())
       {
           OnDestroy?.Invoke(this);
       }
   }
}
