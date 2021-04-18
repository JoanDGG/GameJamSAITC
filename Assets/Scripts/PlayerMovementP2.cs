using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementP2 : MonoBehaviour
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
    private BarraResultadosP2 barraResultadosP2;

    Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        Flip();
        rigidbody2d = GetComponent<Rigidbody2D>();
        barraResultadosP2 = FindObjectOfType<BarraResultadosP2>();
        GameManager.saludes[1] = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y <= -3)
        {
            Perder();
        }

        //Movimiento personaje 2: izquierda, derecha, arriba
        inputMove = Input.GetAxis("Horizontal_P2");
        inputJump = Input.GetButtonDown("Jump_P2");
        inputCrouch = Input.GetKey("down");
        inputAttack = Input.GetButtonDown("Fire2");
        inputStrongAttack = Input.GetKeyDown("page up");
        inputSpecial = Input.GetKeyDown("page down");
        inputShield = Input.GetKey("right shift");

        //Input.GetMouseButtonDown(0);
        child.GetComponent<Animator>().SetBool("Agachado", inputCrouch);
        child.GetComponent<Animator>().SetBool("EnPiso", is_grounded_controllerP2.is_grounded_P2);

        if (inputJump)
        {
            Jump(jumpValue);
            child.GetComponent<Animator>().SetBool("EnPiso", false);
        }

        if (inputAttack)
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
        if (other.gameObject.name == "Hitbox" && other.gameObject != attack && !inputShield)
        {
            print("Ataque!");
            GameManager.saludes[1] -= 5.0f;
            barraResultadosP2.SetValue(GameManager.saludes[1] / vidaMax);
        }
        else if (other.gameObject.tag == "Proyectil" && !inputShield)
        {
            print("Proyectil ha dado en el blanco!");
            GameManager.saludes[1] -= 3.0f;
            print("Sin escudo");
            barraResultadosP2.SetValue(GameManager.saludes[1] / vidaMax);
        }
        if (GameManager.saludes[1] <= 0)
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
        texto.GetComponent<Text>().text = "Ha ganado el jugador 1";
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
        if (!is_grounded_controllerP2.is_grounded_P2)
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

    //Ataque b�sico
    void AtaqueNormal()
    {
        //Hace da�o
        print("Ataque Normal");
    }

    //Ataque con input extra (Shift)
    public IEnumerator AtaqueFuerte()
    {
        //Hace m�s da�o y rompe la defensa del oponente
        print("Ataque Fuerte");
        yield return new WaitForSeconds(0.7f);
        child.GetComponent<Animator>().SetBool("Ataque", false);
    }

    //Ataque mientras est� agachado
    void AtaqueBajo()
    {
        //Hace menos da�o que el ataque normal
        print("Ataque bajo");
    }

    //Ataque mientras est� en el aire
    void AtaqueAire()
    {
        //Hace m�s da�o que el ataque normal, pero menos que el fuerte
        print("Ataque A�reo");
    }

    //Ataque especial
    public IEnumerator AtaqueEspecial()
    {
        if (is_grounded_controllerP2.is_grounded_P2)
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
