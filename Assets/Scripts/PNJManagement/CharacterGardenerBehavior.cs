using UnityEngine;
using System.Collections;

public class CharacterGardenerBehavior : CharacterBehavior
{
    public Transform targetTransform;
    public float speed = 5f;
    private bool isMoving = false;
    public GameObject Mail;

    protected override void HandleDialogueEnd()
    {
        if (!isMoving && targetTransform != null)
        {
            isMoving = true;
            StartCoroutine(MoveToTarget());
        }

        Mail.GetComponent<MailSystem>().enabled = true;
    }

    private IEnumerator MoveToTarget()
    {
        while (transform.position != targetTransform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, speed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
    }
}