using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Linq;
using TMPro;
using UnityEngine.UI;
using static DialogueScientist;

public class DialogueScientist : MonoBehaviour
{
    [SerializeField]
    GameObject prefabMailScientist, prefabMailPlayer, parentMail, verticalScroll, pressEspace, mycroft, timeMachine;
    [SerializeField]
    public int numDial = -1;
    int posYDial = -50;
    string namePlayer;
    public int nbDialogue;
    int unlockPerso;
    static int unlockPersoStatic;
    [SerializeField] List<UnlockPerso> unlockList = new List<UnlockPerso>();
    //class par rapport au json scientistDialogue
    [System.Serializable]
    public class ScientistDialogue
    {
        public int id;
        public string name;
        public string text;
        public int nbLine;
        public bool highlight;
        public string[] highlighteWords;
        public string[] explanationWord;
        public bool isEnd;
    }
    [System.Serializable]
    public class ScientistDialogueList
    {
        public ScientistDialogue[] scientistDialogue0; // premier dialogue
        public ScientistDialogue[] scientistDialogue1; // deuxieme dialogue
        public ScientistDialogue[] scientistDialogue2; // troisieme dialogue
    }
    public ScientistDialogueList data;
    private void Awake()
    {
        //deserialisation du JSON
        string json = File.ReadAllText(Application.streamingAssetsPath + "/" + "Dialogue/ScientistDialogue.json");
        data = JsonConvert.DeserializeObject<ScientistDialogueList>(json);
        //Remplacement de "nomJoueur" dans le Json par le nom entrer dans le menu du jeu
        namePlayer = PlayerPrefs.GetString("namePlayer");
        for (int i = 0; i < data.scientistDialogue0.Length; i++)
        {
            data.scientistDialogue0[i].text = data.scientistDialogue0[i].text.Replace("nomJoueur", namePlayer);
            data.scientistDialogue0[i].name = data.scientistDialogue0[i].name.Replace("nomJoueur", namePlayer);
        }
        for (int i = 0; i < data.scientistDialogue1.Length; i++)
        {
            data.scientistDialogue1[i].text = data.scientistDialogue1[i].text.Replace("nomJoueur", namePlayer);
            data.scientistDialogue1[i].name = data.scientistDialogue1[i].name.Replace("nomJoueur", namePlayer);
        }
        for (int i = 0; i < data.scientistDialogue2.Length; i++)
        {
            data.scientistDialogue2[i].text = data.scientistDialogue2[i].text.Replace("nomJoueur", namePlayer);
            data.scientistDialogue2[i].name = data.scientistDialogue2[i].name.Replace("nomJoueur", namePlayer);
        }
        //Desactivation mycroft parceque pas besoin de le voir tous de suite
        mycroft.SetActive(false);
    }
    private void Start()
    {
        numDial = 0;
        pressEspace.SetActive(true);
        //Affichage du premier dialogue quand script activer au début
        Display(numDial, data.scientistDialogue0);
    }
    // Update is called once per frame
    void Update()
    {
        verticalScroll.GetComponent<Scrollbar>().value = 0;
        //Changement de phrase de dialogue en fonction du numero du dialogue sur lequel on est
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (nbDialogue)
            {
                case 0:
                    Display(numDial + 1, data.scientistDialogue0);
                    break;
                case 1:
                    Display(numDial + 1, data.scientistDialogue1);
                    mycroft.GetComponent<DialogueMycroft>().isMoving = true; //mycroft arrive et se rapproche du joueur pendant se dialogue 
                    mycroft.SetActive(true);
                    break;
                case 2:
                    Display(numDial + 1, data.scientistDialogue2);
                    break;
            }
        }
    }
    //fonction pour afficher un nouveau dialogue
    public void Display(int idtxt, ScientistDialogue[] nameDialogue)
    {
        if (idtxt < nameDialogue.Length)
        {
            numDial = idtxt;
            GameObject mailGo;
            if (nameDialogue[numDial].name == "Scientist")
            {
                mailGo = Instantiate(prefabMailScientist, parentMail.transform);
                mailGo.transform.localPosition = new Vector3(0, posYDial, 0);
            }
            else
            {
                mailGo = Instantiate(prefabMailPlayer, parentMail.transform);
                mailGo.transform.localPosition = new Vector3(1100 + 450, posYDial, 0);
            }
            posYDial += -75 - 30 * nameDialogue[numDial].nbLine;
            Debug.Log(nameDialogue[numDial].nbLine);
            mailGo.GetComponent<mail>().UpdateContenu(nameDialogue[numDial].name, nameDialogue[numDial].text);
            mailGo.GetComponent<RectTransform>().sizeDelta = new Vector2(900, 60 + 30 * nameDialogue[numDial].nbLine);
            parentMail.GetComponent<RectTransform>().sizeDelta = new Vector2(1600, Mathf.Abs(posYDial));
            verticalScroll.GetComponent<Scrollbar>().numberOfSteps = numDial;

            if (nameDialogue[numDial].isEnd)
            {
                pressEspace.SetActive(false);
                if (nbDialogue == 0)
                {
                    timeMachine.GetComponent<BoxCollider2D>().enabled = true;
                    missionsManager.missionManagerInstance.AddMission(0);
                }
            }
            else
            {
                pressEspace.SetActive(true);
            }

        }
    }
}
