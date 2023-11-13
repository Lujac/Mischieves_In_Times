using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailSystem : MonoBehaviour
{
    public GameObject prefabMailScientist; // Prefab du MailScientist à instancier à certains moments
    public GameObject prefabMailPlayer; // Prefab du MailPlayer à instancier à certains moments
    public GameObject parentMail; // L'endroit où les prefabs doivent être instanciés
    public GameObject pressEspace;
    public GameObject verticalScroll;
    public List<MailData> MailList; // Modifier le type de DialogueList en List<DialogueData>
    public int DialogueNumber;
    public int currentLineIndex;
    public int currentMailIndex = 0;
    public int posYDial = 0;
    public static bool dialogueInProgress = false; // Ajouter la déclaration de la variable dialogueInProgress
    public bool Mailinprogress = false;
    public bool MailEnd = false;
    

    void OnEnable()
    {
        // Vérifier si un autre dialogue est déjà en cours
        if (dialogueInProgress) return;

        // Activer ce DialogueSystem comme le DialogueSystem actif
        dialogueInProgress = true;

        // Récupérer le nom du joueur dans les playerprefs "PlayerPrefs.GetString("SelectedCharacter");"
        // pour l'attribuer comme valeur aux valeurs characternames[] de ma classe DialogueData lorsque la valeur est vide

        currentLineIndex = 0; // Initialiser currentLineIndex à 0

        DisplayDialogue();
    }

    void Update()
    {
        if (verticalScroll.GetComponent<Scrollbar>().value < 0 || (verticalScroll.GetComponent<Scrollbar>().value != 0 && Mailinprogress))
        {
            verticalScroll.GetComponent<Scrollbar>().value = 0;
            Mailinprogress = false;
        }
        
        // if (MailEnd)
        // {
        //     Debug.Log("TEST");
        // }
        
        if (verticalScroll.GetComponent<Scrollbar>().value > 1)
        {
            verticalScroll.GetComponent<Scrollbar>().value = 1;
        }

        // Si la touche espace est enfoncée et qu'il y a un dialogue en cours
        if (Input.GetKeyDown(KeyCode.Space) && dialogueInProgress)
        {
            // Si currentLineIndex est inférieur à la taille du tableau dialogueLines pour le dialogue actuel
            if (currentLineIndex < MailList[DialogueNumber].dialogueLines.Length - 1)
            {
                currentLineIndex++; // Augmenter currentLineIndex pour passer à la prochaine ligne du dialogue
                currentMailIndex++;
                DisplayDialogue();
            }
            else
            {
                Mailinprogress = false;
                MailEnd = true;
                dialogueInProgress = false;
            }
            
        }
    }

    public void DisplayDialogue()
    {
        Mailinprogress= true;
        MailData dialogueData = MailList[DialogueNumber];
        GameObject mailGo;

        Debug.Log(dialogueData);
        
        // Instancier des prefabs en fonction du nom du personnage
        if (dialogueData.characterNames[currentLineIndex] == "Scientist")
        {
            mailGo = Instantiate(prefabMailScientist, parentMail.transform);
            mailGo.transform.localPosition = new Vector3(0, posYDial, 0);
           
            GameObject txtMail = mailGo.transform.Find("txt_mail").gameObject;
            TMPro.TextMeshProUGUI textField = txtMail.GetComponent<TMPro.TextMeshProUGUI>();
            textField.text = dialogueData.dialogueLines[currentLineIndex];

            GameObject nameMail = mailGo.transform.Find("name_mail").gameObject;
            TMPro.TextMeshProUGUI nameField = nameMail.GetComponent<TMPro.TextMeshProUGUI>();
            nameField.text = dialogueData.characterNames[currentLineIndex];
           
            Debug.Log("La valeur de PosYdial est" +posYDial);
            Debug.Log("La valeur de Nombreligne est" + dialogueData.NombreLigne[currentLineIndex]);
        }
        else
        {
            mailGo = Instantiate(prefabMailPlayer, parentMail.transform);
            mailGo.transform.localPosition = new Vector3(1100 + 450, posYDial, 0);

            GameObject txtMail = mailGo.transform.Find("txt_mail").gameObject;
            TMPro.TextMeshProUGUI textField = txtMail.GetComponent<TMPro.TextMeshProUGUI>();
            textField.text = dialogueData.dialogueLines[currentLineIndex];

            GameObject nameMail = mailGo.transform.Find("name_mail").gameObject;
            TMPro.TextMeshProUGUI nameField = nameMail.GetComponent<TMPro.TextMeshProUGUI>();

            if (string.IsNullOrEmpty(dialogueData.characterNames[currentLineIndex]))
            {
                nameField.text = PlayerPrefs.GetString("SelectedCharacter");
            }
            else
            {
                nameField.text = dialogueData.characterNames[currentLineIndex];
            }

            Debug.Log("La valeur de PosYdial est" + posYDial);
            Debug.Log("La valeur de Nombreligne est" + dialogueData.NombreLigne[currentLineIndex]);
        }

        // Calculer la valeur de posYDial en fonction du nombre de lignes dans dialogueLines[]
        posYDial += -75 - 35 * dialogueData.NombreLigne[currentLineIndex];

        parentMail.GetComponent<RectTransform>().sizeDelta = new Vector2(1600, Mathf.Abs(posYDial));
        mailGo.GetComponent<RectTransform>().sizeDelta = new Vector2(900, 60 + 30 * dialogueData.NombreLigne[currentLineIndex]);
        
        verticalScroll.GetComponent<Scrollbar>().numberOfSteps = currentMailIndex;
        pressEspace.SetActive(true);
    }


    public void AdvanceDialogue() {
        if(MailEnd) {
            DialogueNumber++;
            currentLineIndex = 0;
            MailEnd = false;
            Mailinprogress = true;
            // DisplayDialogue();
        }
    }
}
