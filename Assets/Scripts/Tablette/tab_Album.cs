using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;
using TMPro;
using UnityEngine.UI;

public class tab_Album : MonoBehaviour
{
    public static tab_Album instance;

    public PersonnageAlbumList data;

    public GameObject panoInfo;

    public int numPano;

    [SerializeField] GameObject prefabPerso, parentPerso, panoTextName, panoTextDesctiption, panoImg;

    [SerializeField] List<Sprite> imgPerso = new List<Sprite>();

    [SerializeField] List<int> numPerso = new List<int>();    

    [System.Serializable]
    public class PersonnageAlbum
    {
        public int id;
        public string name;
        public string description;
    }

    [System.Serializable]
    public class PersonnageAlbumList
    {
        public PersonnageAlbum[] personnageAlbum;
    }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Attention, il y a plus d'une instance de instance dans la scène");
            return;
        }
        instance = this;
        panoInfo.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + "/" + "PersonnageAlbum.json");
        data = JsonConvert.DeserializeObject<PersonnageAlbumList>(json);
        UpdateSizeAlbum();
    }

    public void UpdateSizeAlbum()
    {
        for (int i = 0; i < data.personnageAlbum.Length; i++)
        {
            foreach(int num in numPerso)
            {
                if(i == num)
                {
                    GameObject prefab = Instantiate(prefabPerso, parentPerso.transform);
                    prefab.GetComponent<tab_AlbumPerso>().majPerso(data.personnageAlbum[i].name);
                    prefab.GetComponent<tab_AlbumPerso>().id = data.personnageAlbum[i].id;

                    if (i < imgPerso.Count)
                    {
                        prefab.GetComponent<tab_AlbumPerso>().majPerso(data.personnageAlbum[i].name, imgPerso[i]);
                    }
                    else
                    {
                        prefab.GetComponent<tab_AlbumPerso>().majPerso(data.personnageAlbum[i].name);
                    }
                    break;
                }
            }
        }
    }

    public void QuitPanoInfo()
    {
        panoInfo.SetActive(false);
    }

    public void UpdatePanoInfo()
    {
        panoTextName.GetComponent<TMP_Text>().text = data.personnageAlbum[numPano].name;
        panoTextDesctiption.GetComponent<TMP_Text>().text = data.personnageAlbum[numPano].description;
        panoImg.GetComponent<Image>().sprite = imgPerso[numPano];
    }

    public void UpdateContenu()
    {
        numPerso = PlayerControl.agentMovementInstance.numAlbum;
    }

   
}
