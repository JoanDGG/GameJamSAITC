using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
Descripcion:
Este script revisa si el personaje entra en contacto con alguna zona con el tag piso

Autor: Joan Daniel Guerrero Garcia
*/

public class is_grounded_controllerP2 : MonoBehaviour
{
    public static bool is_grounded_P2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Piso"))
        {
            is_grounded_P2 = true;
            //print("Esta en piso");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Piso"))
        {
            is_grounded_P2 = false;
            //print("No esta en piso");
        }
    }
}
