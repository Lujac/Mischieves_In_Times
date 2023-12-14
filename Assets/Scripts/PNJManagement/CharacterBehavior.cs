using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

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

    protected IEnumerator MoveToTarget(Vector3 newPosition)
    {
        while (transform.position != newPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
        // Destroy(gameObject);
    }

    public void TriggerMovementToTarget()
    {
        if (!isMoving && targetTransform != null)
        {
            isMoving = true;
            StartCoroutine(MoveToTarget(targetTransform.position));
        }
    }

    public void TriggerMovementCustom(Vector3 newPosition)
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveToTarget(newPosition));
        }
    }
}
