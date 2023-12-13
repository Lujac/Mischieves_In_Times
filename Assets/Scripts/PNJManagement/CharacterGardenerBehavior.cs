using UnityEngine;
using System.Collections;

public class CharacterGardenerBehavior : CharacterBehavior
{
    public Transform targetTransform;
    public float speed = 5f;
    private bool isMoving = false;
    [SerializeField] GameObject tabMail, btnTab, tabMenu, TimeMachine;

    protected override void HandleDialogueEnd()
    {
        if (!isMoving && targetTransform != null)
        {
            isMoving = true;
            StartCoroutine(MoveToTarget());
        }

        btnTab.GetComponent<Animator>().SetBool("notif", true);
        tabMenu.GetComponent<tab_menu>().notifbtn[5] = true;
 
        tabMail.GetComponent<MailSystem>().enabled = true; // Mails activés après ce dialogue
        TimeMachine.GetComponent<BoxCollider2D>().enabled = true; // Active l'interaction avec la Time Machine

        DisableDialog();
    }

    private IEnumerator MoveToTarget()
    {
        while (transform.position != targetTransform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
        // Destroy(gameObject);
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