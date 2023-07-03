using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControl : MonoBehaviour
{
    public Tilemap walkableTilemap;
    public float moveSpeed = 5f;
    private Vector2 movement;
    public static PlayerControl agentMovementInstance;
    public Rigidbody2D rb;
    public bool isActive, tabIsActive, dialIsActive, movementEnabled;
    public List<int> numAlbum = new List<int>();
    static List<int> numAlbumStatic = new List<int>();
    [SerializeField] float speedWalk;
    [SerializeField] GameObject tablette, btnTab;

    private void Awake()
    {
        // Une seule instance dans la scène
        if (agentMovementInstance != null)
        {
            Debug.Log("Il y a plus d'une instance de playerMovement dans la scène");
            Destroy(this);
            return;
        }
        agentMovementInstance = this;
        rb = GetComponent<Rigidbody2D>();
        isActive = true;
        tabIsActive = false;
        dialIsActive = false;
        tablette.SetActive(tabIsActive);
        LoadInfo();
    }

      

    private void Update()
    {
        if (movementEnabled)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

        }
       
        else
        {
            movementEnabled = false;
           
        }

        //Activation tablette
        if (Input.GetKeyDown(KeyCode.T) && !dialIsActive)
        {
            tabIsActive = !tabIsActive;
            tablette.SetActive(tabIsActive);
            isActive = !tabIsActive;
        }
        if (tabIsActive)
        {
            btnTab.GetComponent<Animator>().SetBool("notif", false);
            isActive = false;
        }

    }

    private void FixedUpdate()
    {
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.deltaTime;
        movement.Normalize(); // Pour s'assurer que le personnage se déplace à la même vitesse dans toutes les directions
        if (walkableTilemap.HasTile(walkableTilemap.WorldToCell(newPosition)))
        {
            rb.MovePosition(newPosition);
        }
        
    }

    public void SaveInfo() //Enregistrer les infos du joueur
    {
        numAlbumStatic = numAlbum;
    }

    public void LoadInfo() //Charger les infos du joueur
    {
        numAlbum = numAlbumStatic;
    }

    public void tab()
    {
        if (!dialIsActive)
        {
            tabIsActive = !tabIsActive;
            tablette.SetActive(tabIsActive);
            isActive = !tabIsActive;
        }
    }

    public void EnableMovemment()
    {

        movementEnabled = true;
    }

    public void DisableMovemment()
    {

        movementEnabled = false;
    }
}
