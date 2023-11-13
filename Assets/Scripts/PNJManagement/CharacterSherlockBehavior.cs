using UnityEngine;
using System.Collections;

public class CharacterSherlockBehavior : CharacterBehavior
{
    public bool DialogueSherlockGreetCompleted = false;

    protected override void HandleDialogueEnd()
    {
        if(!DialogueSherlockGreetCompleted) {
            
            GameObject btnTablette = GameObject.Find("btnTablette");
            GameObject tabMenu = GameObject.Find("Canvas")
                .transform.Find("Tablette")
                .transform.Find("Tablette")
                .transform.Find("tab_Menu").gameObject; // Nécessaire de passer par les parents car inactif à ce moment

            // Activation des notifs et puzzle du dico
            
            btnTablette.GetComponent<Animator>().SetBool("notif", true);
            tabMenu.GetComponent<tab_menu>().notifbtnOn(0);
            DialogueSherlockGreetCompleted = true;
        
        } /* else if (true) {

        } */
    }
}