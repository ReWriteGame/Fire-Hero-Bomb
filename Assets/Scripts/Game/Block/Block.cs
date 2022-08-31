using Game.Logic.Score;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


[SelectionBase]
public class Block : MonoBehaviour
{
    [SerializeField] private ScoreCounter health;
    [SerializeField] private Vector2 startHealth;
    [SerializeField] private SkinView skins;

    public UnityEvent OnHealthIsOver;
  
  
    private void Start()
    {
        skins.SetRandomSkin();
        health.isMinScoreEvent.AddListener(HealthIsOver);
    }

    private void OnDestroy()
    {
        health.isMinScoreEvent.RemoveListener(HealthIsOver);
    }

    private void HealthIsOver()
    {
        OnHealthIsOver?.Invoke();
    }

    public void SetStartHealth(float value)
    {
        health.Score = (int)value;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Bullet>())
        {
            health.TakeAway(col.gameObject.GetComponent<Bullet>().Damage);
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //if (col.gameObject.GetComponent<Destroyer>())
        //{
        //    Destroy(gameObject);
        //}
    }
}