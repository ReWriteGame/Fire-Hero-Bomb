using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Rebound : MonoBehaviour
{
    public Vector2 result;
    public float radius;
    public LayerMask layer;// s 4em stalkivaca
    private Rigidbody2D rb;




    private RaycastHit2D hit2;
    private Vector2 currentMovementDirection;

    private Vector3 target;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        UpdateDirection();
    }

    private void UpdateDirection()
    {
        currentMovementDirection = rb.velocity;
        hit2 = Physics2D.Raycast(transform.position, currentMovementDirection, 5, layer.value);
    }

    public void ReflectProjectile()
    {
        if (hit2.collider != null)
        {
            target = hit2.transform.position;
            result = Vector2.Reflect(currentMovementDirection, hit2.normal);
            rb.velocity = result.normalized * rb.velocity.magnitude;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Physics2D.OverlapCircle(transform.position, .5f, layer))
        {
            ReflectProjectile();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, result);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
