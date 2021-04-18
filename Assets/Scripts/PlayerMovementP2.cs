using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementP2 : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    public bool facingRight = true;
    private bool movingRight = false;
    private bool movingLeft = false;

    //private bool is_attacking = false;

    //private bool first_attack = true;

    public float movementSpeed = 5;
    public float jumpValue = 5;
    public float vidaMax = 100.0f;
    public float vidaActual;

    public float inputMove;
    public bool inputJump;
    public bool inputAttack;

    public GameObject attack;
    private BarraResultadosP2 barraResultadosP2;

    Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        barraResultadosP2 = FindObjectOfType<BarraResultadosP2>();
        vidaActual = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento personaje 2: izquierda, derecha, arriba
        inputMove = Input.GetAxis("Horizontal_P2");
        inputJump = Input.GetButtonDown("Jump_P2");
        inputAttack = Input.GetButtonDown("Fire2");

        //Input.GetMouseButtonDown(0);

        if (inputJump)
        {
            Jump(jumpValue);
        }

        if (inputAttack)
        {
            attack.SetActive(true);
            StartCoroutine(Atacar());
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Hitbox" && other.gameObject != attack)
        {
            print("Ataque!");
            vidaActual -= 5.0f;
            barraResultadosP2.SetValue(vidaActual / vidaMax);
        }
        else if (other.gameObject.tag == "Proyectil")
        {
            print("Proyectil ha dado en el blanco!");
            vidaActual -= 3.0f;
            barraResultadosP2.SetValue(vidaActual / vidaMax);
        }
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
        if (is_grounded_controllerP2.is_grounded_P2)
        {
            rigidbody2d.AddForce(Vector3.up * force, ForceMode2D.Impulse);
            //Debug.Log("Jump");
            //Debug.Log(force);
        }
    }

    public IEnumerator Atacar()
    {
        yield return new WaitForSeconds(0.3f);
        attack.SetActive(false);
    }
}
