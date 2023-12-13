using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    enum Directions {Haut, Bas, Gauche, Droite}
    [SerializeField] Directions directionBlock;
    [SerializeField] DialogueSystem dialogBlock;
    [SerializeField] float distanceToMove = 1;
    private GameObject player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") {
            dialogBlock.StartDialogue();
            player = collision.gameObject;
        }
    }

    private void MovePlayer()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 newPosition;

        if(directionBlock == Directions.Haut)
            newPosition = playerPosition + new Vector2(0, distanceToMove);
        else if(directionBlock == Directions.Bas)
            newPosition = playerPosition + new Vector2(0, -distanceToMove);
        else if(directionBlock == Directions.Gauche)
            newPosition = playerPosition + new Vector2(-distanceToMove, 0);
        else
            newPosition = playerPosition + new Vector2(distanceToMove, 0);
        
        player.transform.position = newPosition;
    }

    private void Start()
    {
        dialogBlock.OnDialogueEnd += MovePlayer;
    }

    private void OnDestroy()
    {
        dialogBlock.OnDialogueEnd -= MovePlayer;
    }
}
