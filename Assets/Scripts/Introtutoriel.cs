using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introtutoriel : MonoBehaviour
{
    public PlayerControl playerControl;

    void Start()
    {
        playerControl.EnableMovemment();
    }
   
}