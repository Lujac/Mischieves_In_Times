using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroStart : MonoBehaviour

  
{

    public DialogueSystem DialogueIntro;
    // Start is called before the first frame update
    void Start()
    {
        DialogueIntro.StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
