using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Escudo : MonoBehaviour
{

    private BarraResultadosP1 barraResultadosP1;
    private BarraResultadosP2 barraResultadosP2;
    public GameObject anuncio;
    public GameObject texto;

    void Start()
    {
        barraResultadosP1 = FindObjectOfType<BarraResultadosP1>();
        barraResultadosP2 = FindObjectOfType<BarraResultadosP2>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int jugador = (gameObject.name == "Escudo1") ? 0 : 1;
        if ((other.gameObject.name == "Hitbox1" && jugador !=0) || (other.gameObject.name == "Hitbox2" && jugador != 1))
        {
            print("Ataque!");
            GameManager.saludes[jugador] -= 2.5f;
            if (jugador == 0)
            {
                barraResultadosP1.SetValue(GameManager.saludes[jugador] / 100.0f);
            }
            else
            {
                barraResultadosP2.SetValue(GameManager.saludes[jugador] / 100.0f);
            }
        }
        else if (other.gameObject.tag == "Proyectil")
        {
            print("Proyectil ha dado en el Escudo!");
            GameManager.saludes[jugador] -= 0.75f;
            if (jugador == 0)
            {
                barraResultadosP1.SetValue(GameManager.saludes[jugador] / 100.0f);
            }
            else
            {
                barraResultadosP2.SetValue(GameManager.saludes[jugador] / 100.0f);
            }
        }
        if (GameManager.saludes[0] <= 0)
        {
            Time.timeScale = 0.0f;
            anuncio.SetActive(true);
            texto.GetComponent<Text>().text = "Ha ganado el jugador 2";
        }
        else if (GameManager.saludes[1] <= 0)
        {
            Time.timeScale = 0.0f;
            anuncio.SetActive(true);
            texto.GetComponent<Text>().text = "Ha ganado el jugador 1";
        }
    }
}
