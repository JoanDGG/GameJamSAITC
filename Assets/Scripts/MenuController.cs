using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Jugar(){
        SceneManager.LoadScene("Combate");
    }
    public void SeleccionPersonaje(){
        SceneManager.LoadScene("SeleccionPersonaje");
    }
    public void Menu(){
        SceneManager.LoadScene("MainMenu");
    }
}
