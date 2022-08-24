using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Animations
{
    public class LoopRotateAnimation : MonoBehaviour
    {
        [SerializeField] private Vector3 direction;
        [SerializeField][Min(0)] private float speed;
        [Space]
        [SerializeField] private bool playOnAwake;
        [SerializeField] private bool animated;
        
        private Transform transformObject;

        
        private void Awake()
        {
            transformObject = GetComponent<Transform>();
            if (playOnAwake) Play();
        }

        private IEnumerator Start()
        {
            while (true)
            {
               if(animated) transformObject.Rotate(direction.normalized * speed);
               yield return null;
            }
        }

        //private void FixedUpdate()
        //{
        //    if(animated) transformObject.Rotate(direction.normalized * speed);
        //}

        public void Play()
        {
            animated = true;
        }

        public void Stop()
        {
            animated = false;
        }
    }
}