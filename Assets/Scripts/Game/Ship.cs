using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class Ship : MonoBehaviour
{
 [SerializeField] private Fire fire;
 [SerializeField][Min(0)] private int countBullets;
 [SerializeField][Min(0)] private float speedFire;


 private Coroutine coroutine;
 private Rigidbody2D rb;
 
 public UnityEvent OnDeath;
 public UnityEvent OnFire;
 
 
 public Rigidbody2D Rb => rb;


 private void Awake()
 {
     rb = GetComponent<Rigidbody2D>();
     StartFire();
 }

 public void StartFire()
 {
     StopFire();
     coroutine = StartCoroutine(FireRoutine());
 }
 
 public void StopFire()
 {
     if(coroutine != null)StopCoroutine(coroutine);
 }
 

 private void OnCollisionEnter2D(Collision2D col)
 {
     if (col.gameObject.GetComponent<Asteroid>())
     {
        // Death();
     }
     
   
 }

 private void OnTriggerEnter2D(Collider2D col)
 {
     if (col.gameObject.GetComponent<Block>())
     {
         Death();
     }
 }

 private void Death()
 {
     gameObject.SetActive(false);
     OnDeath?.Invoke();
 }



 private IEnumerator FireRoutine()
 {
     var delay = new WaitForSeconds(speedFire);

     while (true)
     {
         fire.FireShotForward(countBullets);
         OnFire?.Invoke();
         yield return delay;
     }
 }
}
