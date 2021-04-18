using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    public bool facingRight = true;
    private bool movingRight = false;
    private bool movingLeft = false;
    public GameObject proyectiles;
    public GameObject anuncio;
    public GameObject texto;
    public GameObject escudo;

    //private bool is_attacking = false;

    //private bool first_attack = true;

    public float movementSpeed = 5;
    public float jumpValue = 5;
    public float vidaMax = 100.0f;

    public float inputMove;
    public bool inputJump;
    public bool inputAttack;
    public bool inputCrouch;
    public bool inputStrongAttack;
    public bool inputSpecial;
    public bool inputShield;

    public GameObject attack;
    public GameObject child;
    private BarraResultadosP1 barraResultadosP1;
    
    Vector2 velocity;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        barraResultadosP1 = FindObjectOfType<BarraResultadosP1>();
        GameManager.saludes[0] = vidaMax;
        child.GetComponent<Animator>().SetInteger("Personaje", GameManager.personajesSeleccionados[0]);
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -3)
        {
            Perder();
        }
        
        //Movimiento personaje 1: A, D, W
        inputMove = Input.GetAxis("Horizontal");
        inputJump = Input.GetButtonDown("Jump");
        inputCrouch = Input.GetKey("s");
        inputAttack = Input.GetButtonDown("Fire1");
        inputStrongAttack = Input.GetKeyDown("left alt");
        inputSpecial = Input.GetKeyDown("e");
        inputShield = Input.GetKey("q");

        //Input.GetMouseButtonDown(0);

        child.GetComponent<Animator>().SetBool("Agachado", inputCrouch);
        child.GetComponent<Animator>().SetBool("EnPiso", is_grounded_controller.is_grounded);

        if (inputJump)
        {
            Jump(jumpValue);
            child.GetComponent<Animator>().SetBool("EnPiso", false);
        }

        if(inputAttack)
        {
            attack.SetActive(true);
            child.GetComponent<Animator>().SetBool("Ataque", true);
            StartCoroutine(Atacar());
        }

        if (inputStrongAttack)
        {
            attack.SetActive(true);
            child.GetComponent<Animator>().SetBool("Ataque", true);
            StartCoroutine(AtaqueFuerte());
        }

        if (inputSpecial)
        {
            attack.SetActive(true);
            child.GetComponent<Animator>().SetBool("AtaqueEspecial", true);
            StartCoroutine(AtaqueEspecial());
        }


        if (inputShield)
        {
            escudo.SetActive(true);
        }
        else
        {
            escudo.SetActive(false);
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
        if (other.gameObject.name == "Hitbox2" && !inputShield)
        {
            print("Ataque!");
            GameManager.saludes[0] -= 5.0f;
            barraResultadosP1.SetValue(GameManager.saludes[0] / vidaMax);
        }
        else if (other.gameObject.tag == "Proyectil" && !inputShield)
        {
            print("Proyectil ha dado en el blanco!");
            GameManager.saludes[0] -= 1.5f;
            barraResultadosP1.SetValue(GameManager.saludes[0] / vidaMax);
        }
        if (GameManager.saludes[0] <= 0)
        {
            Perder();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }

    void Perder()
    {
        Time.timeScale = 0.0f;
        anuncio.SetActive(true);
        texto.GetComponent<Text>().text = "Ha ganado el jugador 2";
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
        if (!is_grounded_controller.is_grounded)
        {
            AtaqueAire();
        }
        else if (inputCrouch)
        {
            AtaqueBajo();
        }
        else
        {
            AtaqueNormal();
        }
        yield return new WaitForSeconds(0.3f);
        attack.SetActive(false);
        child.GetComponent<Animator>().SetBool("Ataque", false);
    }
    
    //Ataque básico
    void AtaqueNormal()
    {
        //Hace daño
        print("Ataque Normal");
    }

    //Ataque con input extra (Shift)
    public IEnumerator AtaqueFuerte()
    {
        //Hace más daño y rompe la defensa del oponente
        print("Ataque fuerte");
        yield return new WaitForSeconds(0.7f);
        child.GetComponent<Animator>().SetBool("Ataque", false);
    }

    //Ataque mientras esté agachado
    void AtaqueBajo()
    {
        //Hace menos daño que el ataque normal
        print("Ataque bajo");
    }

    //Ataque mientras esté en el aire
    void AtaqueAire()
    {
        //Hace más daño que el ataque normal, pero menos que el fuerte
        print("Ataque Aéreo");
    }

    ////Ataque especial
    public IEnumerator AtaqueEspecial()
    {
        if (is_grounded_controller.is_grounded)
        {
            if (inputCrouch)
            {
                AtaqueEspecialBajo();
            }
            else
            {
                AtaqueEspecialNormal();
            }
        }
        yield return new WaitForSeconds(0.3f);
        child.GetComponent<Animator>().SetBool("AtaqueEspecial", false);
    }

    void AtaqueEspecialBajo()
    {
        print("AtaqueEspecialBajo");
    }

    void AtaqueEspecialNormal()
    {
        print("AtaqueEspecialNormal");
    }

    public IEnumerator AtaqueProyectil()
    {
        GameObject proyectil = (GameObject)Instantiate(proyectiles);
        if (!facingRight)
        {
            proyectil.transform.Rotate(Vector3.up, 180.0f, Space.World);
        }
        float velocidad = 3.0f;
        float distancia = 1.75f;
        Rigidbody2D rigidbody2dProyectil = proyectil.GetComponent<Rigidbody2D>();
        rigidbody2dProyectil.velocity = (facingRight) ? new Vector2(velocidad, rigidbody2dProyectil.velocity.y) : new Vector2(-velocidad, rigidbody2dProyectil.velocity.y);
        proyectil.transform.position = (facingRight) ? new Vector2(transform.position.x + distancia, transform.position.y) : new Vector2(transform.position.x - distancia, transform.position.y);
        yield return new WaitForSeconds(0.2f);
    }
}
