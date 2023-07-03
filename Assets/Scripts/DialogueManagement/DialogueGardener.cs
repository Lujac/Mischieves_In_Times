using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.AI;

public class DialogueGardener : MonoBehaviour
{
    [SerializeField]
    GameObject goDialogue, txtName, txtDialogue, pressE, pointExclamation, player, btnPrefab, parentbtn, panobtns, btnEye, btnEyeSprite, targetGardener, tabMail, btnTab, btnMail, tab;
    [SerializeField]
    Sprite sprEyeOn, sprEyeOff;
    [SerializeField]
    bool dialIsActive, endDialogue, isActive, isNext, isMoving;
    [SerializeField]
    int numDial;
    string namePlayer;
     float speed = 5f;

    [System.Serializable]
    public class GardenerDialogue
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
    public class GardenerDialogueList
    {
        public GardenerDialogue[] gardenerDialogue;
    }
    public GardenerDialogueList data;

    
    // Start is called before the first frame update
    void Start()
    {
        numDial = 0;
        string json = File.ReadAllText(Application.streamingAssetsPath + "/" + "Dialogue/GardenerDialogue.json");
        data = JsonConvert.DeserializeObject<GardenerDialogueList>(json);
        dialIsActive = false;
        pressE.SetActive(false);
        isActive = false;
        isMoving= false;
        namePlayer = PlayerPrefs.GetString("namePlayer");
        panobtns.SetActive(false);
        for (int i = 0; i < data.gardenerDialogue.Length; i++)
        {
            data.gardenerDialogue[i].text = data.gardenerDialogue[i].text.Replace("nomJoueur", namePlayer);
            data.gardenerDialogue[i].name = data.gardenerDialogue[i].name.Replace("nomJoueur", namePlayer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            Move(targetGardener.transform.position);
            tabMail.GetComponent<DialogueScientist>().enabled = true;
            btnTab.GetComponent<Animator>().SetBool("notif", true);
            tab.GetComponent<tab_menu>().notifbtnOn(5);
            pressE.SetActive(false);
        }
        if (dialIsActive && Input.GetKeyDown(KeyCode.E))
        {
            goDialogue.SetActive(true);
            dialIsActive = false;
            pressE.SetActive(false);
            DisplayDialogue(numDial);
            isActive = true;
            pointExclamation.SetActive(false);
        }
        if (isActive && Input.GetKeyDown(KeyCode.Space))
        {
            
            if (numDial + 1 < data.gardenerDialogue.Length)
            {
                numDial++;
                DisplayDialogue(numDial);
            }
            else if (numDial + 1 == data.gardenerDialogue.Length || endDialogue)
            {
                goDialogue.SetActive(false);
                numDial = 0;
                PlayerControl.agentMovementInstance.isActive = true;
                PlayerControl.agentMovementInstance.dialIsActive = false;
                isActive= false;
                if (isNext)
                {
                    pressE.SetActive(true);
                    dialIsActive = true;
                }
                else
                {
                    dialIsActive = false;
                }
                isMoving = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
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
        if (data.gardenerDialogue[numDial].highlight)
        {
            btnEye.GetComponent<Button>().interactable = true;
            btnEyeSprite.GetComponent<Image>().sprite = sprEyeOn;
            for (int i = 0; i < data.gardenerDialogue[numDial].highlighteWords.Length; i++)
            {
                GameObject wordButton = Instantiate(btnPrefab, parentbtn.transform);
                wordButton.transform.localPosition = new Vector3(-200 + 400 * i, 0, 0);
                //wordButton.GetComponent<btnWord>().loadbtn(data.gardenerDialogue[numDial].highlighteWords[i], data.gardenerDialogue[numDial].explanationWord[i]);

            }
        }
        else
        {
            btnEye.GetComponent<Button>().interactable = false;
            btnEyeSprite.GetComponent<Image>().sprite = sprEyeOff;
        }
        txtName.GetComponent<TMP_Text>().text = data.gardenerDialogue[numDial].name;
        endDialogue = data.gardenerDialogue[numDial].isEnd;
        txtDialogue.GetComponent<TMP_Text>().text = data.gardenerDialogue[numDial].text;

    }
    public void DisplayWordOn()
    {
        panobtns.SetActive(true);
    }
    public void DisplayWordOff()
    {
        panobtns.SetActive(false);
    }

    public void Move(Vector3 target)
    {
        if (transform.position == target)
        {
            isMoving = false;
            this.gameObject.SetActive(false);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }
    }
}
