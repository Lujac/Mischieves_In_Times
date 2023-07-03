using System.Collections.Generic;
using UnityEngine;

public class MailSystem : MonoBehaviour
{
    public GameObject prefabMailScientist; // Prefab du MailScientist à instancier à certains moments
    public GameObject prefabMailPlayer; // Prefab du MailPlayer à instancier à certains moments
    public GameObject parentMail; // L'endroit où les prefabs doivent être instanciés
    public List<MailData> MailList; // Modifier le type de DialogueList en List<DialogueData>
    public int DialogueNumber;
    private int currentLineIndex;
    private int posYDial = 0;
    private static bool dialogueInProgress = false; // Ajouter la déclaration de la variable dialogueInProgress

    void Start()
    {
        // Vérifier si un autre dialogue est déjà en cours
        if (dialogueInProgress)
        {
            // Ignorer le démarrage du dialogue si un autre dialogue est déjà en cours
            return;
        }

        // Activer ce DialogueSystem comme le DialogueSystem actif
        dialogueInProgress = true;

        // Récupérer le nom du joueur dans les playerprefs "PlayerPrefs.GetString("SelectedCharacter");"
        // pour l'attribuer comme valeur aux valeurs characternames[] de ma classe DialogueData lorsque la valeur est vide

        currentLineIndex = 0; // Initialiser currentLineIndex à 0

        DisplayDialogue();
    }

    void Update()
    {
        // Si la touche espace est enfoncée et qu'il n'y a pas de dialogue en cours
        if (Input.GetKeyDown(KeyCode.Space) && dialogueInProgress)
        {
            // Si currentLineIndex est inférieur à la taille du tableau dialogueLines pour le dialogue actuel
            if (currentLineIndex < MailList[DialogueNumber].dialogueLines.Length - 1)
            {
                currentLineIndex++; // Augmenter currentLineIndex pour passer à la prochaine ligne du dialogue

                DisplayDialogue();
            }
            else
            {
                // Faire ce que tu veux lorsque le dialogue actuel est terminé
            }
        }
    }

    void DisplayDialogue()
    {
        MailData dialogueData = MailList[DialogueNumber];
        GameObject mailGo;
        
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
        posYDial += -75 - 35 * dialogueData.NombreLigne[currentLineIndex];
        parentMail.GetComponent<RectTransform>().sizeDelta = new Vector2(1600, Mathf.Abs(posYDial));
        mailGo.GetComponent<RectTransform>().sizeDelta = new Vector2(900, 60 + 30 * dialogueData.NombreLigne[currentLineIndex]);
        // Calculer la valeur de posYDial en fonction du nombre de lignes dans dialogueLines[]

    }

    
}
