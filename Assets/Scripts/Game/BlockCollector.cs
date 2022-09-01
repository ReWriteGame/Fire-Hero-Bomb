using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BlockCollector : MonoBehaviour
{
   [SerializeField] private GameObject blockCollector;
   [SerializeField] private GameObject visual;
   [SerializeField] private float size;
   [SerializeField] private float timeDuration;

   private CircleCollider2D collider;
   private Coroutine coroutine;
   private void Start()
   {
      collider = GetComponent<CircleCollider2D>();
      collider.enabled = false;
      visual.SetActive(false);
   }

   public void Activate()
   {
      StartRoutine();
      collider.enabled = true;
      visual.SetActive(true);
   }

   private void StartRoutine()
   {
      if(coroutine != null)StopCoroutine(coroutine);
      coroutine = StartCoroutine(ResizeRoutine());
   }

   private IEnumerator ResizeRoutine()
   {
      for (float time = timeDuration; time > 0; time -= Time.deltaTime)
      {
         float value = 1 - time / timeDuration;
         yield return null;

         blockCollector.transform.localScale = Vector3.one * Mathf.Lerp(1, size, value);

      }
   }
   
   
}
