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
        word = GetComponentInChildren<TextMeshProUGUI>().text;

        foreach (DialogueSystem dialogueSystem in dialogueSystems)
        {
            WordDefinition wordDefinition = dialogueSystem.GetWordDefinition(word);

            if (wordDefinition != null)
            {
                definitionText.text = wordDefinition.definition;
                wordImage.sprite = wordDefinition.sprite;

                definitionText.gameObject.SetActive(true);
                wordImage.gameObject.SetActive(true);
            }
        }
    }

    public void OnPointerExit()
    {
        definitionText.gameObject.SetActive(false);
        wordImage.gameObject.SetActive(false);
    }
}
