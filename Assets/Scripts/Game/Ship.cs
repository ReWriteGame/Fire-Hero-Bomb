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

 private void Update()
 {
     //RotationAnimation(transform);
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
 
 [Space, Header("Rotation Settings")] [SerializeField]
 private float maxRotationAngle = 45;
 [SerializeField] private float startRotationAngle = 90;
 [SerializeField] private float rotationStrengthMultiplier = 1;
 [SerializeField, Range(0f, 1f)] private float rotationSmoothness = 0.2f;

 private void Start()
 {
     StartCoroutine(RotationAnimationRoutine());
 }


 private IEnumerator RotationAnimationRoutine()
 {
     var delay = new WaitForFixedUpdate();
     var transform = this.transform;
     while (gameObject)
     {
         float lastPositionX = transform.position.x;
         yield return delay;
         float sizeDelta = transform.position.x - lastPositionX;
         RotationAnimation(transform, -sizeDelta);
     }
 }

 private void RotationAnimation(Transform obj, float sideDelta)
 {
     //Rotation Rendering
     var rotation = obj.eulerAngles;
     var nextRotation = startRotationAngle + sideDelta * rotationStrengthMultiplier;
     nextRotation = Mathf.Clamp(nextRotation, startRotationAngle - maxRotationAngle, startRotationAngle + maxRotationAngle);
     nextRotation = Mathf.Lerp(rotation.y, nextRotation, rotationSmoothness);
     rotation.y = nextRotation;
     obj.eulerAngles = rotation;
 }
}
