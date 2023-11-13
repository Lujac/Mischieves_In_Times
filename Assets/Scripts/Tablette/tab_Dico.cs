using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tab_Dico : MonoBehaviour
{
    CharacterSherlockBehavior SherlockState;

    void OnEnable()
    {
        if(!SherlockState) SherlockState = GameObject.Find("PNJ_Sherlock").GetComponent<CharacterSherlockBehavior>();
        
        Debug.Log(SherlockState.DialogueSherlockGreetCompleted);
    }
}
