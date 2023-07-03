using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class mail : MonoBehaviour
{
    [SerializeField]
    GameObject txtName, txtDialogue;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateContenu(string name, string dialogue)
    {
        txtName.GetComponent<TMP_Text>().text = name;
        txtDialogue.GetComponent<TMP_Text>().text = dialogue;

    }
}
