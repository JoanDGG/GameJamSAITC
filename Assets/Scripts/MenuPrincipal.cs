using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{

    public GameObject continuar;

    void Start()
    {
        GameManager.jugador = 1;
    }
    
    //Jugador seleccion� un bot�n que progresa el juego, hace transici�n de la escena actual a la siguiente
    public void Avanzar()
    {
        print("Avanzando de escena");
        GameManager.escena++;
        SceneManager.LoadScene(GameManager.escena);
    }

    //Jugador seleccion� un bot�n que retrasa el juego, hace transici�n de la escena actual a la anterior
    public void Retroceder()
    {
        print("Retrocediendo de escena");
        GameManager.escena--;
        SceneManager.LoadScene(GameManager.escena);
    }

    //Jugador seleccion� salir, el juego se cierra
    public void Salir()
    {
        print("Gracias por jugar");
        Application.Quit();
    }

    //Proceso de selecci�n de personajes
    public void SeleccionarPersonaje(int personaje)
    {
        print("Personaje Seleccionado");
        GameObject boton = (GameObject) GameObject.Find("Canvas/Personaje"+personaje.ToString() + "/Text");
        print("El jugador " + GameManager.jugador.ToString() + " seleccion� a " + boton.GetComponent<Text>().text);
        GameManager.personajesSeleccionados[GameManager.jugador - 1] = personaje;
        bool Continuar = (GameManager.jugador >= 2) ? true : false;
        continuar.SetActive(Continuar);
        GameManager.jugador += 1;
    }
}
