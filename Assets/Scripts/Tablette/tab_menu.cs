using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class tab_menu : MonoBehaviour
{
    [SerializeField] List<GameObject> appList = new List<GameObject>();
    [SerializeField] List<GameObject> btnList = new List<GameObject>();
    [SerializeField] GameObject txtPlayerName;   
    public List<bool> notifbtn = new List<bool>(); //0 tabMap, 1 tab Inventaire, 2 tab missions, 3 tab Album, 4 tab Journal, 5 tab Mail
    private void Awake()
    {
        txtPlayerName.GetComponent<TMP_Text>().text = PlayerPrefs.GetString("namePlayer");
        for (int i = 0; i<6; i++)
        {
            notifbtn.Add(false);
        }
        //Une seule instance dans la sc�ne
        
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject al in appList)
        {
            al.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i =0; i<notifbtn.Count; i++)
        {
            if (notifbtn[i])
            {
                notifbtnOn(i);
            }
        }
    }
    public void openApp(GameObject windows)
    {
        windows.SetActive(true);
    }
    public void CloseApp(GameObject windows)
    {
        windows.SetActive(false);
    }
    public void CloseTab(GameObject windows)
    {
        // Clic sur la croix
        
        windows.SetActive(false);
        PlayerControl.agentMovementInstance.tabIsActive = false;
        PlayerControl.agentMovementInstance.isActive = true;
        PlayerControl.agentMovementInstance.EnableMovemment();
    }

    public void notifbtnOn(int i) {
        if (i < btnList.Count && i < notifbtn.Count)
        {
            btnList[i].GetComponent<Animator>().SetBool("notif", true);
            notifbtn[i] = true;
        }
    }
    public void notifbtnOff(int i)
    {
        if (i < btnList.Count && i < notifbtn.Count)
        {
            notifbtn[i] = false;
            btnList[i].GetComponent<Animator>().SetBool("notif", false);
        }
    }
}
