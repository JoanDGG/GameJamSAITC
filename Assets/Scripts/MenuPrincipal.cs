using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class MenuPrincipal : MonoBehaviour
{

    public GameObject continuar;
    private GameObject anuncio;
    private Text textoAnuncio;
    public GameObject[] cambios = new GameObject[2];

    void Start()
    {
        GameManager.jugador = 1;
        anuncio = (GameObject) GameObject.Find("Canvas/AnuncioJugador");
        textoAnuncio = anuncio.GetComponent<Text>();
        textoAnuncio.text = ("El jugador " + GameManager.jugador.ToString() + " está seleccionando personaje");
    }
    
    //Jugador seleccionó un botón que progresa el juego, hace transición de la escena actual a la siguiente
    public void Avanzar()
    {
        print("Avanzando de escena");
        GameManager.escena++;
        SceneManager.LoadScene(GameManager.escena);
    }

    //Jugador seleccionó un botón que retrasa el juego, hace transición de la escena actual a la anterior
    public void Retroceder()
    {
        print("Retrocediendo de escena");
        GameManager.escena--;
        SceneManager.LoadScene(GameManager.escena);
    }

    //Jugador seleccionó salir, el juego se cierra
    public void Salir()
    {
        print("Gracias por jugar");
        Application.Quit();
    }

    //Proceso de selección de personajes
    public void SeleccionarPersonaje(int personaje)
    {
        if (GameManager.personajesSeleccionados.Contains(0))
        {
            print("Personaje Seleccionado");
            GameObject boton = (GameObject)GameObject.Find("Canvas/Personajes/Personaje" + personaje.ToString() + "/Text");
            print("El jugador " + GameManager.jugador.ToString() + " seleccionó a " + boton.GetComponent<Text>().text);
            GameManager.personajesSeleccionados[GameManager.jugador - 1] = personaje;
            bool Continuar = GameManager.personajesSeleccionados.Contains(0);
            continuar.SetActive(!Continuar);
            cambios[GameManager.jugador - 1].SetActive(true);
            GameManager.jugador += 1;
            textoAnuncio.text = (GameManager.personajesSeleccionados.Contains(0)) ? ("El jugador " + GameManager.jugador.ToString() + " está seleccionando personaje") : ("Personajes seleccionados");
        }
        else
        {
            continuar.SetActive(true);
            textoAnuncio.text = ("Personajes seleccionados");
            print("No puedes seleccionar más");
        }
    }

    public void CambiarPersonaje(int jugador)
    {
        print("El jugador " + jugador.ToString() + " va a cambiar de personaje");
        GameManager.jugador = jugador;
        GameManager.personajesSeleccionados[GameManager.jugador - 1] = 0;
        continuar.SetActive(false);
        textoAnuncio.text = ("El jugador " + GameManager.jugador.ToString() + " está seleccionando personaje");
    }
}
