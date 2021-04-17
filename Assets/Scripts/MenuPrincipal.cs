using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    //Jugador seleccion� jugar, hace transici�n del men� a la selecci�n de personajes
    public void Jugar()
    {
        print("Cambiando de escena");
        SceneManager.LoadScene("SeleccionPersonaje");
    }

    //Jugador seleccion� salir, el juego se cierra
    public void Salir()
    {
        print("Gracias por jugar");
        Application.Quit();
    }
}
