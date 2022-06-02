using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // PATRON SINGLETON
    public static GameManager Instance; // A static reference to the GameManager instance

    void Awake()
    {
        if(Instance == null) // If there is no instance already
        {
            DontDestroyOnLoad(gameObject); // Keep the GameObject, this component is attached to, across different scenes
            Instance = this;
        } else if(Instance != this) // If there is already an instance and it's not `this` instance
        {
            Destroy(gameObject); // Destroy the GameObject, this component is attached to
        }
    }

    // VARIABLES GLOBALES
    public static int escena = 0;
    public static int[] personajesSeleccionados = new int[2];
    public static int jugador = 1;
    public static float[] saludes = new float[2];
    public static void Salir(){
        Application.Quit();  
    }
}
