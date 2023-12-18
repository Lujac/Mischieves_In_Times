using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mail : MonoBehaviour
{
    [SerializeField]
    GameObject txtName, txtDialogue;

    public void UpdateContenu(string name, string dialogue)
    {
        txtName.GetComponent<TMP_Text>().text = name;
        txtDialogue.GetComponent<TMP_Text>().text = dialogue;
    }
}
