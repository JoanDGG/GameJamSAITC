using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    //Jugador seleccionó jugar, hace transición del menú a la selección de personajes
    public void Jugar()
    {
        print("Cambiando de escena");
        SceneManager.LoadScene("SeleccionPersonaje");
    }

    //Jugador seleccionó salir, el juego se cierra
    public void Salir()
    {
        print("Gracias por jugar");
        Application.Quit();
    }
}
