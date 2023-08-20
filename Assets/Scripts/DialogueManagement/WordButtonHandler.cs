using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordButtonHandler : MonoBehaviour
{
    public TextMeshProUGUI definitionText;
    public Image wordImage;
    public DialogueSystem[] dialogueSystems; // Tableau de DialogueSystem

    private string word;

    public void OnPointerEnter()
    {
        TextMeshProUGUI textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

        if (textMeshPro != null)
        {
            word = textMeshPro.text;

            foreach (DialogueSystem dialogueSystem in dialogueSystems)
            {
                WordDefinition wordDefinition = dialogueSystem.GetWordDefinition(word);

                if (wordDefinition != null)
                {
                    definitionText.text = wordDefinition.definition;
                    wordImage.sprite = wordDefinition.sprite;

                    definitionText.gameObject.SetActive(true);
                    wordImage.gameObject.SetActive(true);

                    // Vérifier s'il y a une image et du texte dans WordDefinition
                    if (wordDefinition.sprite != null && !string.IsNullOrEmpty(wordDefinition.definition))
                    {
                        // Appliquer l'alignement et le style de texte spécifié
                        definitionText.alignment = TextAlignmentOptions.Bottom;
                        definitionText.fontStyle = FontStyles.Bold;
                    }
                    else
                    {
                        
                    }
                }
            }
        }
    }

    public void OnPointerExit()
    {
        definitionText.gameObject.SetActive(false);
        wordImage.gameObject.SetActive(false);
    }
}
