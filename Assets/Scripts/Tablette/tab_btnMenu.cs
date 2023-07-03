using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tab_btnMenu : MonoBehaviour
{
    public void openApp()
    {
        this.GetComponent<Animator>().SetBool("notif", false);
    }
}
