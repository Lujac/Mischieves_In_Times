using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleportation : MonoBehaviour
{
    public string nomDeLaSceneACharger; // Le nom de la scène à charger

    // Cette fonction est appelée lorsque le joueur entre en collision avec l'objet
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Vérifie si le joueur a collisionné
        {
            // Charge la nouvelle scène en utilisant son nom
            SceneManager.LoadScene(nomDeLaSceneACharger);
        }
    }
}

