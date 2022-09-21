using UnityEngine;

public class LinerMove : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField][Min(0)] private float speed;

    private Transform transform;
    private void Awake()
    {
        transform = GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        transform.Translate(direction.normalized * speed * Time.fixedDeltaTime);
    }
}
