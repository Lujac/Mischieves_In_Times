using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    public DialogueSystem dialogueSystem;
    public GameObject interactPrompt;
    private bool canInteract = false;

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            dialogueSystem.StartDialogue();
            interactPrompt.SetActive(false);
            canInteract = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactPrompt.SetActive(true);
            canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interactPrompt.SetActive(false);
            canInteract = false;
        }
    }
}
