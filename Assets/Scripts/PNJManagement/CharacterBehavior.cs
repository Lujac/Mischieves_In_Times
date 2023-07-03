using UnityEngine;

public abstract class CharacterBehavior : MonoBehaviour
{
    public DialogueSystem dialogueSystem;

    private void Start()
    {
        dialogueSystem.OnDialogueEnd += HandleDialogueEnd;
    }

    private void OnDestroy()
    {
        dialogueSystem.OnDialogueEnd -= HandleDialogueEnd;
    }

    protected abstract void HandleDialogueEnd();
}
