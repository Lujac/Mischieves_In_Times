using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeMachine : MonoBehaviour
{
    [SerializeField]
    GameObject tabMail, btnTab, btnMail, tab;
    // Start is called before the first frame update
    void Start()
    {
        //Box collider desactiver pour qu'il ne detecte pas le joueur tant que c'est pas le moment
        GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //detection entrer du joueur dans le collider de la machine pour activer un dialogue avec la scientifique
        if(collision.tag == "Player")
        {
            btnTab.GetComponent<Animator>().SetBool("notif", true);

            tab.GetComponent<tab_menu>().notifbtn[5] = true;
            tabMail.GetComponent<MailSystem>().DialogueNumber = 1;
            tabMail.GetComponent<MailSystem>().DisplayDialogue();
            GetComponent<BoxCollider2D>().enabled = false;
            missionsManager.missionManagerInstance.SupprMission(0);
        }
    }
}
