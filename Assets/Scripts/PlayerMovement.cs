using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    public bool facingRight = true;
    private bool movingRight = false;
    private bool movingLeft = false;

    //private bool is_attacking = false;

    //private bool first_attack = true;

    public float movementSpeed = 5;
    public float jumpValue = 5;

    public float inputMove;
    public bool inputJump;
    public bool inputAttack;

    Vector2 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento personaje 1: A, D, W
        if (gameObject.name == "Personaje1")
        {
            inputMove = Input.GetAxis("Horizontal");
            inputJump = Input.GetButtonDown("Jump");
        }
        //Movimiento personaje 2: izquierda, derecha, arriba
        else if (gameObject.name == "Personaje2")
        {
            inputMove = Input.GetAxis("Horizontal_P2");
            inputJump = Input.GetButtonDown("Jump_P2");
        }
        //inputAttack = Input.GetMouseButtonDown(0);
        
        if (inputJump)
        {
            Jump(jumpValue);
        }

        rigidbody2d.velocity = new Vector2(inputMove * movementSpeed, rigidbody2d.velocity.y);

        if ((inputMove > 0.0f && !facingRight) || (inputMove < 0.0f && facingRight))
        {
            Flip();
        }

        if (movingRight)
        {
            rigidbody2d.velocity = new Vector2(movementSpeed, rigidbody2d.velocity.y);
            if (!facingRight)
            {
                Flip();
            }

        }
        if (movingLeft)
        {
            rigidbody2d.velocity = new Vector2(-movementSpeed, rigidbody2d.velocity.y);
            if (facingRight)
            {
                Flip();
            }
        }
        //if (inputAttack || is_attacking)
        //{
        //    if (first_attack)
        //    {
        //        animator.SetBool("attacking", true);
        //        //Debug.Log("Ataque 1");
        //        first_attack = false;
        //    }
        //    else
        //    {
        //        animator.SetBool("attacking2", true);
        //        //Debug.Log("Ataque 2");
        //        first_attack = true;
        //    }
        //}
        //else
        //{
        //    if (!first_attack)
        //    {
        //        animator.SetBool("attacking", false);
        //    }
        //    else
        //    {
        //        animator.SetBool("attacking2", false);
        //    }
        //}

        //if (is_attacking || fire_active)
        //{
        //    is_attacking = false;
        //    fire_active = false;
        //}
    }

    void FixedUpdate()
    {

        
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }

    //public void Attack()
    //{
    //    is_attacking = true;
    //}

    public void Jump(float force)
    {
        if (gameObject.name == "Personaje1" && is_grounded_controller.is_grounded)
        {
            rigidbody2d.AddForce(Vector3.up * force, ForceMode2D.Impulse);
            //Debug.Log("Jump");
            //Debug.Log(force);
        }
        else if (gameObject.name == "Personaje2" && is_grounded_controllerP2.is_grounded_P2)
        {
            rigidbody2d.AddForce(Vector3.up * force, ForceMode2D.Impulse);
            //Debug.Log("Jump");
            //Debug.Log(force);
        }
    }
}
