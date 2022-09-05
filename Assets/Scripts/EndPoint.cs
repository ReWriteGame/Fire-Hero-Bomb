using UnityEngine;
using UnityEngine.Events;

public class EndPoint : MonoBehaviour
{
    public UnityEvent OnPlayer;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Ship>())
        {
            OnPlayer?.Invoke();
        }
    }
}
