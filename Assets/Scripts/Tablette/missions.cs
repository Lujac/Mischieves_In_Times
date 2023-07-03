using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class missions : MonoBehaviour
{
    [SerializeField]
    GameObject txtName, txtDescription, bgDescription;
    public void UpdateContenu(string name, string dialogue, int nbLine)
    {
        txtName.GetComponent<TMP_Text>().text = name;
        txtDescription.GetComponent<TMP_Text>().text = dialogue;
        bgDescription.GetComponent<RectTransform>().sizeDelta = new Vector2(320, 10 + nbLine * 32.5f);
    }
}
