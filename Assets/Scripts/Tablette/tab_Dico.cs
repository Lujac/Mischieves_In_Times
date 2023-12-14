using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tab_Dico : MonoBehaviour
{
    CharacterSherlockBehavior SherlockState;
    GameObject PopupDico;
    GameObject PuzzleDico;

    void OnEnable()
    {
        if(!SherlockState) SherlockState = GameObject.Find("PNJ_Sherlock").GetComponent<CharacterSherlockBehavior>();
        if(!PopupDico) PopupDico = this.gameObject.transform.GetChild(0).Find("PopupDico").gameObject;
        if(!PuzzleDico) PuzzleDico = this.gameObject.transform.GetChild(0).Find("PuzzleDico").gameObject;

        if(SherlockState.DialogueSherlockGreetCompleted && !PopupDico.activeSelf) PopupDico.SetActive(true);
        if(SherlockState.DialogueSherlockGreetCompleted && !PuzzleDico.activeSelf) PuzzleDico.SetActive(true);
        
        // Debug.Log(SherlockState.DialogueSherlockGreetCompleted);
    }
}
