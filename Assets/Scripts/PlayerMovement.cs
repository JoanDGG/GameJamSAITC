using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    public bool facingRight = true;
    private bool movingRight = false;
    private bool movingLeft = false;
    public GameObject proyectiles;

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
    private BarraResultadosP1 barraResultadosP1;
    
    Vector2 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        barraResultadosP1 = FindObjectOfType<BarraResultadosP1>();
        vidaActual = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento personaje 1: A, D, W
        inputMove = Input.GetAxis("Horizontal");
        inputJump = Input.GetButtonDown("Jump");
        inputAttack = Input.GetButtonDown("Fire1");
        
        //Input.GetMouseButtonDown(0);

        if (inputJump)
        {
            Jump(jumpValue);
        }

        if(inputAttack)
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
            barraResultadosP1.SetValue(vidaActual / vidaMax);
        }
        else if (other.gameObject.tag == "Proyectil")
        {
            print("Proyectil ha dado en el blanco!");
            vidaActual -= 3.0f;
            barraResultadosP1.SetValue(vidaActual / vidaMax);
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
        if (is_grounded_controller.is_grounded)
        {
            rigidbody2d.AddForce(Vector3.up * force, ForceMode2D.Impulse);
            //Debug.Log("Jump");
            //Debug.Log(force);
        }
    }

    public IEnumerator Atacar()
    {
        yield return new WaitForSeconds(0.3f);
        GameObject proyectil = (GameObject)Instantiate(proyectiles);
        if (!facingRight)
        {
            proyectil.transform.Rotate(Vector3.up, 180.0f, Space.World);
        }
        float velocidad = 3.0f;
        float distancia = 1.5f;
        Rigidbody2D rigidbody2dProyectil = proyectil.GetComponent<Rigidbody2D>();
        rigidbody2dProyectil.velocity = (facingRight) ? new Vector2(velocidad, rigidbody2dProyectil.velocity.y) : new Vector2(-velocidad, rigidbody2dProyectil.velocity.y);
        proyectil.transform.position = (facingRight) ? new Vector2(transform.position.x + distancia, transform.position.y) : new Vector2(transform.position.x - distancia, transform.position.y);
        attack.SetActive(false);
    }
}
