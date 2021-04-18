using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Contiene las funciones que hacen que el pausa funcione, script asociado al canvas.

public class Pausa : MonoBehaviour
{
    public AudioSource musicaFondo;
    // Musica de pausa.
    public AudioSource musicaPausa;

    public void Pausar() {
        musicaFondo.Stop();
        this.transform.GetChild(5).gameObject.SetActive(true);
        musicaPausa.Play();
        Time.timeScale = 0;
    }

    public void CambiarAMenu() {
        SceneManager.LoadScene("Menu");
        GameManager.escena = 0;
    }

    public void Continuar() {
        musicaPausa.Stop();
        musicaFondo.Play();
        this.transform.GetChild(5).gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Start()
    {
        // Desactivar el panel de pausa el inicio.
        this.transform.GetChild(5).gameObject.SetActive(false);
    }
}
