using UnityEngine;
using System.Collections;

public abstract class CharacterBehavior : MonoBehaviour
{
    public DialogueSystem dialogueSystem;
    public Transform targetTransform;
    public bool isMoving = false;
    public float speed = 5f;

    protected void Start()
    {
        dialogueSystem.OnDialogueEnd += HandleDialogueEnd;
    }

    protected void OnDestroy()
    {
        dialogueSystem.OnDialogueEnd -= HandleDialogueEnd;
    }

    protected abstract void HandleDialogueEnd();

    protected IEnumerator MoveToTarget()
    {
        while (transform.position != targetTransform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
        // Destroy(gameObject);
    }

    public void TriggerMovement()
    {
        if (!isMoving && targetTransform != null)
        {
            isMoving = true;
            StartCoroutine(MoveToTarget());
        }
    }
}
