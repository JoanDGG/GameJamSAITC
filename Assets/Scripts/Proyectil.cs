using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{

    private Rigidbody2D rigidbody2d;
    private float velocidad = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
        rigidbody2d = GetComponent<Rigidbody2D>();
        bool Derecha = false;
        rigidbody2d.velocity = (Derecha) ? new Vector2(velocidad, rigidbody2d.velocity.y) : new Vector2(-velocidad, rigidbody2d.velocity.y);
        if (Derecha)
        {
            Flip();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    void Flip()
    {
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }
}
