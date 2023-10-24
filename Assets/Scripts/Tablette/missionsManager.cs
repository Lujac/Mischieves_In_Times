using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class missionsManager : MonoBehaviour
{
    [SerializeField] GameObject prefabMission;
    public static missionsManager missionManagerInstance;
    public float posY = 0;
    public int nbTrue = 0;
    [System.Serializable]
    public class Missions
    {
        public int id;
        public string name;
        public string text;
        public int nbLine;
        public bool state;
    }
    [System.Serializable]
    public class MissionsList
    {
        public Missions[] missions; 
    }
    public MissionsList data;
    private void Awake()
    {
        //Une seule instance dans la sc�ne
        if (missionManagerInstance != null)
        {
            Debug.Log("Il y a plus d'une instance de playerMovement dans la scene");
            Destroy(this);
            return;
        }
        missionManagerInstance = this;
    }
    void Start()
    {
        string json = File.ReadAllText(Application.streamingAssetsPath + "/" + "missions.json");
        data = JsonConvert.DeserializeObject<MissionsList>(json);
    }
    public void MajMission()
    {
        posY = 500;
        nbTrue =0;
        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i<data.missions.Length; i++)
        {
            if (data.missions[i].state == true)
            {
                print(i);
                GameObject prefMission = Instantiate(prefabMission, this.transform);
                prefMission.transform.localPosition = new Vector3(-150, posY - (75 + data.missions[i].nbLine * 32.5f )* nbTrue, 0);
                prefMission.GetComponent<missions>().UpdateContenu(data.missions[i].name, data.missions[i].text, data.missions[i].nbLine);
                nbTrue++;
            }
        }
    }
    public void AddMission(int id)
    {
        data.missions[id].state= true;
        MajMission();
    }

    public void SupprMission(int id)
    {
        data.missions[id].state = false;
        MajMission();
    }
}
