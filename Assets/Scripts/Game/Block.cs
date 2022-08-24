using Game.Logic.Score;
using UnityEngine;
using Random = UnityEngine.Random;


[SelectionBase]
public class Block : MonoBehaviour
{
  [SerializeField] private ScoreCounter health;
  [SerializeField] private Vector2 startHealth;
  private void Start()
  {
    SetStartHealth(Random.Range(startHealth.x, startHealth.y));
  }

  public void SetStartHealth(float value)
  {
    health.Score = (int)value;
  }

  private void OnTriggerEnter2D(Collider2D col)
  {
    if (col.gameObject.GetComponent<Bullet>())
    {
    }

  }
}
