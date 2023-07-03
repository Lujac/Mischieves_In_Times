// Resume : Ce script est utilise pour gerer un systeme de dialogue dans un jeu Unity.
// Il permet d'afficher et de controler le deroulement du dialogue, la mise en gras des mots specifiques a faire apprendre aux eleves, la gestion d'une boite speciale de dialogue pour voir la definition/ l'image qui correspond a un mot,
// la recherche de definitions de mots. Il fournit egalement des fonctionnalites pour l'activation/desactivation du mouvement du personnage joueur,
// et declenche un evenement a la fin du dialogue.

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    // References aux elements d'interface utilisateur utilises pour afficher le dialogue et les caracteres associes.
    public TextMeshProUGUI dialogueText, charaname;
    public DialogueData dialogueData;
    public Image BoiteDialogue, BoiteChara;

    // Indice pour suivre la ligne de dialogue actuelle.
    private int currentLineIndex;
    private static DialogueSystem activeDialogueSystem; // Référence au DialogueSystem actif
    private static bool dialogueInProgress; // Indicateur si un dialogue est en cours


    // Reference au composant PlayerControl pour contrôler le personnage du joueur.
    public PlayerControl playerControl;

    // Tableau de mots qui seront mis en gras dans le dialogue.
    public string[] customBoldWords;

    // References aux elements d'interface utilisateur pour le bouton d'activation/desactivation des mots en gras.
    public Image EyeButtonImage;
    public Button EyeButton;
    public Sprite EyeClosed;
    public Sprite EyeOpen;

    // References aux GameObjects utilises pour gerer une boite speciale de dialogue.
    public GameObject Outside, Inside;

    // Tableau de TextMeshProUGUI pour afficher les mots en gras dans l'interface utilisateur.
    public TextMeshProUGUI[] boldWordsTexts;

    // Booleen indiquant si le dialogue se deroule dans une boîte spéciale.
    public bool inSpecialBox = false;

    // Liste de definitions de mots.
    public List<WordDefinition> wordDefinitions;

    // Evenement déclenche a la fin du dialogue.
    public event Action OnDialogueEnd;

    // Methode appelee a chaque frame.
    
    
    private void Update()
    {
       
        // Si la touche espace est enfoncee et le dialogue ne se deroule pas dans une boîte speciale, continuer le dialogue.
        if (dialogueInProgress && activeDialogueSystem == this && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueDialogue();
           
        }

        // Desactiver le controle du personnage du joueur si la boite de dialogue est active, sinon l'activer.
        if (BoiteDialogue.gameObject.activeSelf)
        {
            playerControl.DisableMovemment();
        }
        else
        {
            playerControl.EnableMovemment();
        }
    }

    // Methode pour demarrer le dialogue.
    public void StartDialogue()
    {
        

        // Vérifier si un autre dialogue est déjà en cours
        if (dialogueInProgress && activeDialogueSystem != this)
        {
            // Ignorer le démarrage du dialogue si un autre dialogue est déjà en cours
            return;
        }

        // Activer ce DialogueSystem comme le DialogueSystem actif
        activeDialogueSystem = this;
        dialogueInProgress = true;
       
        // Reinitialiser l'indice de ligne de dialogue.
        currentLineIndex = 0;

        // Afficher le premier dialogue et les elements d'interface associes.
        dialogueText.text = ApplyRichTextTags(dialogueData.dialogueLines[currentLineIndex]);

        // Verifier si le nom du personnage est vide ou non, puis l'afficher.
        if (string.IsNullOrEmpty(dialogueData.characterNames[currentLineIndex]))
        {
            charaname.text = PlayerPrefs.GetString("SelectedCharacter");
        }
        else
        {
            charaname.text = dialogueData.characterNames[currentLineIndex];
        }

        // Activer les elements d'interface du dialogue.
        dialogueText.gameObject.SetActive(true);
        charaname.gameObject.SetActive(true);
        BoiteDialogue.gameObject.SetActive(true);
        BoiteChara.gameObject.SetActive(true);

        // Verifier si le dialogue contient des mots en gras et mettre a jour le bouton correspondant.
        bool containsBoldWords = CheckIfDialogueContainsBoldWords(dialogueData.dialogueLines[currentLineIndex]);
        EyeButton.interactable = containsBoldWords;

        if (containsBoldWords)
        {
            EyeButtonImage.sprite = EyeOpen;
        }
        else
        {
            EyeButtonImage.sprite = EyeClosed;
        }

        // Mettre a jour la liste des mots en gras dans l'interface utilisateur.
        UpdateBoldWordsList(dialogueData.dialogueLines[currentLineIndex]);
    }

    // Methode pour continuer le dialogue.
    public void ContinueDialogue()
    {
        // Verifier si ce n'est pas la derniere ligne du dialogue.
        if (currentLineIndex < dialogueData.dialogueLines.Length - 1)
        {
            // Passer a la ligne suivante du dialogue.
            currentLineIndex++;
            dialogueText.text = ApplyRichTextTags(dialogueData.dialogueLines[currentLineIndex]);

            // Verifier si le nom du personnage est vide ou non, puis l'afficher.
            if (string.IsNullOrEmpty(dialogueData.characterNames[currentLineIndex]))
            {
                charaname.text = PlayerPrefs.GetString("SelectedCharacter");
            }
            else
            {
                charaname.text = dialogueData.characterNames[currentLineIndex];
            }

            // Verifier si la ligne de dialogue contient des mots en gras et mettre a jour le bouton correspondant.
            bool containsBoldWords = CheckIfDialogueContainsBoldWords(dialogueData.dialogueLines[currentLineIndex]);
            EyeButton.interactable = containsBoldWords;

            if (containsBoldWords)
            {
                EyeButtonImage.sprite = EyeOpen;
                Debug.Log("Contient des mots gras");
            }
            else
            {
                EyeButtonImage.sprite = EyeClosed;
                Debug.Log("Ne contient pas de mots gras");
            }

            // Mettre a jour la liste des mots en gras dans l'interface utilisateur.
            UpdateBoldWordsList(dialogueData.dialogueLines[currentLineIndex]);
        }
        else
        {
            // Si c'est la derniere ligne du dialogue, masquer les elements d'interface du dialogue.
            dialogueInProgress = false; // Aucun dialogue en cours
            dialogueText.gameObject.SetActive(false);
            BoiteDialogue.gameObject.SetActive(false);
            charaname.gameObject.SetActive(false);
            BoiteChara.gameObject.SetActive(false);

            // Declencher l'evenement de fin du dialogue.
            OnDialogueEnd?.Invoke();
        }
    }

    // Methode pour verifier si la ligne de dialogue contient des mots en gras definis dans customBoldWords.
    private bool CheckIfDialogueContainsBoldWords(string text)
    {
        foreach (string word in customBoldWords)
        {
            if (text.Contains(word))
            {
                return true;
            }
        }
        return false;
    }

    // Methode pour ajouter des balises de mise en forme et mettre en gras les mots definis dans customBoldWords.
    private string ApplyRichTextTags(string text)
    {
        foreach (string word in customBoldWords)
        {
            text = text.Replace(word, "<b>" + word + "</b>");
        }
        return text;
    }

    // Methode pour mettre a jour la liste des mots en gras a afficher dans l'interface utilisateur.
    private void UpdateBoldWordsList(string dialogueLine)
    {
        List<string> boldWordsList = new List<string>();

        foreach (string word in customBoldWords)
        {
            if (dialogueLine.Contains(word))
            {
                boldWordsList.Add(word);
            }
        }

        for (int i = 0; i < boldWordsTexts.Length; i++)
        {
            if (i < boldWordsList.Count)
            {
                boldWordsTexts[i].text = boldWordsList[i];
                boldWordsTexts[i].gameObject.SetActive(true);
            }
            else
            {
                boldWordsTexts[i].gameObject.SetActive(false);
            }
        }
    }

    // Methode pour activer la boite speciale de dialogue.
    public void EnterSpecialBox()
    {
        Inside.SetActive(true);
        Outside.SetActive(false);
        inSpecialBox = true;
    }

    // Methode pour quitter la boite speciale de dialogue.
    public void LeaveSpecialBox()
    {
        Outside.SetActive(true);
        Inside.SetActive(false);
        inSpecialBox = false;
    }

    // Methode pour obtenir la definition d'un mot.
    public WordDefinition GetWordDefinition(string word)
    {
        return wordDefinitions.Find(w => w.word == word);
    }
}