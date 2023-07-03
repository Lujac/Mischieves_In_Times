using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class tab_AlbumPerso : MonoBehaviour
{
    [SerializeField]
    GameObject txtName, img;
    public int id;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void majPerso(string stName)
    {
        txtName.GetComponent<TMP_Text>().text = stName;
    }
    public void majPerso(string stName, Sprite spr)
    {
        txtName.GetComponent<TMP_Text>().text = stName;
        img.GetComponent<Image>().sprite = spr;
    }

    public void DisplayInfo()
    {
        tab_Album.instance.numPano = id;
        tab_Album.instance.UpdatePanoInfo();
        tab_Album.instance.panoInfo.SetActive(true);

    }
}
