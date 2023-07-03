using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPerso : MonoBehaviour
{
    [SerializeField]
    int numPerso;
    [SerializeField]
    GameObject tabAlbum, newPersoAlbum, btnAlbum, tab;
    bool isActive;
    Animator animatorNewPerso;

    private void Start()
    {
        animatorNewPerso = newPersoAlbum.GetComponent<Animator>();
    }
    private void Update()
    {
        if(isActive && Input.GetKeyDown(KeyCode.E))
        {
            Unlock();
            
        }
    }
    public void Unlock() 
    { 
        PlayerControl.agentMovementInstance.numAlbum.Add(numPerso);
        tabAlbum.GetComponent<tab_Album>().UpdateContenu();
        tabAlbum.GetComponent<tab_Album>().UpdateSizeAlbum();
        animatorNewPerso.SetBool("On", true);
        tab.GetComponent<tab_menu>().notifbtnOn(3);

        StartCoroutine(PersoUnlock());
        
       //tab_Album.instance.UpdatePanoInfo();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isActive = false;
        }
    }

    IEnumerator PersoUnlock()
    {
        yield return new WaitForSeconds(1);
        animatorNewPerso.SetBool("On", false);
        this.gameObject.SetActive(false);   
    }
}
