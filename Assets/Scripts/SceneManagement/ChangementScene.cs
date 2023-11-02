using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementScene : MonoBehaviour
{
    [SerializeField] GameObject aAfficher;

    [SerializeField] string strSceneSortie;
    enum Directions {Haut, Bas, Gauche, Droite}
    [SerializeField] Directions directionSortie;

    void Start()
    {
        aAfficher.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            aAfficher.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            checkExit(collision.transform.position);
            aAfficher.SetActive(false);
        }
    }

    private void checkExit(Vector3 playerPosition)
    {
        if (strSceneSortie == SceneManager.GetActiveScene().name) {
            Debug.Log("Changement de scène vers celle déjà présente");
            return;
        }

        // Debug.Log(playerPosition);
        // Debug.Log(transform.position);
        // Debug.Log(transform.localScale);
        
        // playerPosition > x ou y du trigger pour le côté choisi

        if(
            (directionSortie == Directions.Haut && playerPosition.y <= transform.position.y + transform.localScale.y / 2) ||
            (directionSortie == Directions.Bas && playerPosition.y <= transform.position.y - transform.localScale.y / 2) ||
            (directionSortie == Directions.Gauche && playerPosition.x <= transform.position.x - transform.localScale.x / 2) ||
            (directionSortie == Directions.Droite && playerPosition.x >= transform.position.x + transform.localScale.x / 2)
          ) {
            PlayerControl.agentMovementInstance.SaveInfo();
            SceneManager.LoadScene(strSceneSortie);
        }
    }
}
