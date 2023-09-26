using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using System.IO;
using static DialogueMycroft;
using System.Linq;

public class DialogueMycroft : MonoBehaviour
{
    [SerializeField]
    GameObject goDialogue, txtName, txtDialogue, pressE, pointExclamation, player, btnPrefab, parentbtn, panobtns, btnEye, btnEyeSprite,targetMycroft, dialScientist, btnTab, btnMail , tab;
    [SerializeField]
    Sprite sprEyeOn, sprEyeOff;
    bool dialIsActive, endDialogue, isActive, isNext;
    public bool isMoving;
    int numDial;
    string namePlayer;

    [System.Serializable]
    public class MycroftDialogue
    {
        public int id;
        public string name;
        public string text;
        public bool highlight;
        public string[] highlighteWords;
        public string[] explanationWord;
        public bool isEnd;
    }

    [System.Serializable]
    public class MycroftDialogueList
    {
        public MycroftDialogue[] mycroftDialogue;
    }
    public MycroftDialogueList data;


    // Start is called before the first frame update
    void Start()
    {
        numDial = 0;
        string json = File.ReadAllText(Application.streamingAssetsPath + "/" + "Dialogue/MycroftDialogue.json");
        data = JsonConvert.DeserializeObject<MycroftDialogueList>(json);
        dialIsActive = false;
        pressE.SetActive(false);
        isActive = false;
        namePlayer = PlayerPrefs.GetString("namePlayer");
        panobtns.SetActive(false);
        for (int i = 0; i<data.mycroftDialogue.Length; i++)
        {
            data.mycroftDialogue[i].text = data.mycroftDialogue[i].text.Replace("nomJoueur", namePlayer);
            data.mycroftDialogue[i].name = data.mycroftDialogue[i].name.Replace("nomJoueur", namePlayer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Move();
        }
            if (dialIsActive && Input.GetKeyDown(KeyCode.E))
        {
            goDialogue.SetActive(true);
            dialIsActive = false;
            pressE.SetActive(false);
            DisplayDialogue(numDial);
            isActive = true;
            pointExclamation.GetComponent<Animator>().SetBool("isFind", true);
        }

        if(isActive && Input.GetKeyDown(KeyCode.Space))
        {
            if(numDial+1 < data.mycroftDialogue.Length)
            {
                numDial++;
                DisplayDialogue(numDial);
            }else if(numDial +1 == data.mycroftDialogue.Length || endDialogue)
            {
                goDialogue.SetActive(false);
                numDial = 0;
                PlayerControl.agentMovementInstance.isActive = true;
                PlayerControl.agentMovementInstance.dialIsActive = false;
                missionsManager.missionManagerInstance.AddMission(1);
                isActive = false;
                if (isNext)
                {
                    pressE.SetActive(true);
                    dialIsActive = true;
                }
                else
                {
                    dialIsActive = false;
                }
                if (dialScientist.GetComponent<DialogueScientist>().nbDialogue != 2)
                {
                    btnTab.GetComponent<Animator>().SetBool("notif", true);
                    tab.GetComponent<tab_menu>().notifbtnOn(5);
                    dialScientist.GetComponent<DialogueScientist>().nbDialogue= 2;
                    dialScientist.GetComponent<DialogueScientist>().Display(0, dialScientist.GetComponent<DialogueScientist>().data.scientistDialogue2);
                }
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            dialIsActive = true;
            pressE.SetActive(true);
            isNext = true;
        }
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            dialIsActive = false;
            pressE.SetActive(false);
            isNext = false;
        }
    }

    public void DisplayDialogue(int idtxt)
    {
        goDialogue.SetActive(true);
        PlayerControl.agentMovementInstance.isActive = false;
        PlayerControl.agentMovementInstance.dialIsActive = true;
        numDial = idtxt;
        foreach (Transform child in parentbtn.transform)
        {
            Destroy(child.gameObject);
        }
        if (data.mycroftDialogue[numDial].highlight)
        {
            btnEye.GetComponent<Button>().interactable = true;
            btnEyeSprite.GetComponent<Image>().sprite = sprEyeOn;
            for (int i = 0; i < data.mycroftDialogue[numDial].highlighteWords.Length; i++)
            {
                GameObject wordButton = Instantiate(btnPrefab,parentbtn.transform);
                wordButton.transform.localPosition = new Vector3(-200+400*i, 0, 0);
                //wordButton.GetComponent<btnWord>().loadbtn(data.mycroftDialogue[numDial].highlighteWords[i], data.mycroftDialogue[numDial].explanationWord[i]);
            }
        }
        else
        {
            btnEye.GetComponent<Button>().interactable = false;
            btnEyeSprite.GetComponent<Image>().sprite = sprEyeOff;
        }
        txtName.GetComponent<TMP_Text>().text = data.mycroftDialogue[numDial].name;
        endDialogue = data.mycroftDialogue[numDial].isEnd;
        txtDialogue.GetComponent<TMP_Text>().text = data.mycroftDialogue[numDial].text;
    }

    public void DisplayWordOn()
    {
        panobtns.SetActive(true);
    }
    public void DisplayWordOff()
    {
        panobtns.SetActive(false);
    }

    public void Move()
    {
        if (transform.position == targetMycroft.transform.position)
        {
            isMoving = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetMycroft.transform.position, 4f * Time.deltaTime);
        }
    }
}
