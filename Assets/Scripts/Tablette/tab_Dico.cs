using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tab_Dico : MonoBehaviour
{
    CharacterSherlockBehavior SherlockState;
    GameObject PuzzleDico;

    void OnEnable()
    {
        if(!SherlockState) SherlockState = GameObject.Find("PNJ_Sherlock").GetComponent<CharacterSherlockBehavior>();
        if(!PuzzleDico) PuzzleDico = this.gameObject.transform.GetChild(0).Find("PuzzleDico").gameObject;

        if(SherlockState.DialogueSherlockGreetCompleted && !PuzzleDico.activeSelf) PuzzleDico.SetActive(true);
        
        // Debug.Log(SherlockState.DialogueSherlockGreetCompleted);
    }
}
