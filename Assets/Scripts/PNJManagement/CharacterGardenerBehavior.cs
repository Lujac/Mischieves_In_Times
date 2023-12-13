using UnityEngine;
using System.Collections;

public class CharacterGardenerBehavior : CharacterBehavior
{
    [SerializeField] GameObject tabMail, btnTab, tabMenu, TimeMachine;

    protected override void HandleDialogueEnd()
    {
        TriggerMovement();

        btnTab.GetComponent<Animator>().SetBool("notif", true);
        tabMenu.GetComponent<tab_menu>().notifbtn[5] = true;
 
        tabMail.GetComponent<MailSystem>().enabled = true; // Mails activés après ce dialogue
        TimeMachine.GetComponent<BoxCollider2D>().enabled = true; // Active l'interaction avec la Time Machine

        DisableDialog();
    }

    /* Disables only the dialog trigger collider */
    private void DisableDialog()
    {
        BoxCollider2D[] boxCollider2Ds = GetComponents<BoxCollider2D>();

        foreach (BoxCollider2D collider in boxCollider2Ds)
        {
            if(collider.isTrigger) collider.enabled = false;
        }

        transform.GetChild(0).gameObject.SetActive(false);
    }
}