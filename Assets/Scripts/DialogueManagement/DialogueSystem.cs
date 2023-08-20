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
    public DialogueData[] dialogueDatas; // Liste des DialogueData disponibles
    public int dialogueNumber = 0;
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
        if (!inSpecialBox && dialogueInProgress && activeDialogueSystem == this && Input.GetKeyDown(KeyCode.Space))
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

        // Vérifier si le numéro de dialogue est valide
        

        // Activer ce DialogueSystem comme le DialogueSystem actif
        activeDialogueSystem = this;
        dialogueInProgress = true;

        // Réinitialiser l'indice de ligne de dialogue.
        currentLineIndex = 0;

        // Obtenir le DialogueData correspondant au numéro de dialogue
        DialogueData dialogueData = dialogueDatas[dialogueNumber];

        // Afficher le premier dialogue et les éléments d'interface associés.
        dialogueText.text = ApplyRichTextTags(dialogueData.dialogueLines[currentLineIndex]);

        // Vérifier si le nom du personnage est vide ou non, puis l'afficher.
        if (string.IsNullOrEmpty(dialogueData.characterNames[currentLineIndex]))
        {
            charaname.text = PlayerPrefs.GetString("SelectedCharacter");
        }
        else
        {
            charaname.text = dialogueData.characterNames[currentLineIndex];
        }

        // Activer les éléments d'interface du dialogue.
        dialogueText.gameObject.SetActive(true);
        charaname.gameObject.SetActive(true);
        BoiteDialogue.gameObject.SetActive(true);
        BoiteChara.gameObject.SetActive(true);

        // Vérifier si le dialogue contient des mots en gras et mettre à jour le bouton correspondant.
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

        // Mettre à jour la liste des mots en gras dans l'interface utilisateur.
        UpdateBoldWordsList(dialogueData.dialogueLines[currentLineIndex]);
    }

    // Methode pour continuer le dialogue.
    public void ContinueDialogue()
    {
        // Vérifier si ce n'est pas la dernière ligne du dialogue.
        if (currentLineIndex < dialogueDatas[dialogueNumber].dialogueLines.Length - 1)
        {
            // Passer à la ligne suivante du dialogue.
            currentLineIndex++;
            dialogueText.text = ApplyRichTextTags(dialogueDatas[dialogueNumber].dialogueLines[currentLineIndex]);

            // Vérifier si le nom du personnage est vide ou non, puis l'afficher.
            if (string.IsNullOrEmpty(dialogueDatas[dialogueNumber].characterNames[currentLineIndex]))
            {
                charaname.text = PlayerPrefs.GetString("SelectedCharacter");
            }
            else
            {
                charaname.text = dialogueDatas[dialogueNumber].characterNames[currentLineIndex];
            }

            // Vérifier si la ligne de dialogue contient des mots en gras et mettre à jour le bouton correspondant.
            bool containsBoldWords = CheckIfDialogueContainsBoldWords(dialogueDatas[dialogueNumber].dialogueLines[currentLineIndex]);
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

            // Mettre à jour la liste des mots en gras dans l'interface utilisateur.
            UpdateBoldWordsList(dialogueDatas[dialogueNumber].dialogueLines[currentLineIndex]);
        }
        else
        {
            // Si c'est la dernière ligne du dialogue, masquer les éléments d'interface du dialogue.
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

    // Methode pour mettre à jour la liste des mots en gras dans l'interface utilisateur.
    private void UpdateBoldWordsList(string text)
    {
        // Par defaut, desactiver tous les mots en gras.
        foreach (TextMeshProUGUI boldWordText in boldWordsTexts)
        {
            boldWordText.gameObject.SetActive(false);
        }

        // Vérifier si le texte est vide.
        if (string.IsNullOrEmpty(text))
        {
            // Aucun texte à traiter, donc retourner.
            return;
        }

        // Parcourir tous les mots definis dans customBoldWords.
        foreach (string word in customBoldWords)
        {
            // Vérifier si le mot est présent dans la ligne de dialogue.
            if (text.Contains(word))
            {
                // Rechercher toutes les occurrences du mot dans la ligne de dialogue.
                int startIndex = 0;
                while (startIndex < text.Length)
                {
                    int wordIndex = text.IndexOf(word, startIndex, StringComparison.OrdinalIgnoreCase);
                    if (wordIndex == -1)
                    {
                        break;
                    }

                    // Mettre en gras le mot dans la ligne de dialogue et l'afficher dans l'interface utilisateur.
                    string boldText = $"<b>{word}</b>";
                    text = text.Substring(0, wordIndex) + boldText + text.Substring(wordIndex + word.Length);
                    foreach (TextMeshProUGUI boldWordTextInstance in boldWordsTexts)
                    {
                        if (!boldWordTextInstance.gameObject.activeSelf)
                        {
                            boldWordTextInstance.gameObject.SetActive(true);
                            boldWordTextInstance.text = word;
                            break;
                        }
                    }

                    startIndex = wordIndex + boldText.Length;
                }
            }
        }

        // Mettre à jour la ligne de dialogue avec les mots en gras.
        dialogueText.text = ApplyRichTextTags(text);
    }
    // Methode pour appliquer les balises de texte riches aux mots en gras.
    private string ApplyRichTextTags(string text)
    {
        foreach (string word in customBoldWords)
        {
            text = text.Replace(word, $"<b>{word}</b>");
        }
        return text;
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