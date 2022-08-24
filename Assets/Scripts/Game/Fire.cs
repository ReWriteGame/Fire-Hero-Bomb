using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
  [SerializeField] private List<Bullet> bullets;
  [SerializeField] [Min(0)] private int countBulletsPerShot;
  
  
}
