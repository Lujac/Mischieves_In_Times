using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public Tilemap walkableTilemap;
    public float moveSpeed = 5.5f;
    private Vector2 movement;
    public static PlayerControl agentMovementInstance;
    public Rigidbody2D rb;
    public bool isActive, tabIsActive, dialIsActive, movementEnabled;
    public List<int> numAlbum = new List<int>();
    static List<int> numAlbumStatic = new List<int>();
    [SerializeField] GameObject tablette, btnTab;
    
    public enum Directions {Haut, Bas, Gauche, Droite}

    private void Awake()
    {
        // Une seule instance dans la sc�ne
        if (agentMovementInstance != null)
        {
            Debug.Log("Il y a plus d'une instance de playerMovement dans la sc�ne");
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
        } else {
            movement.x = 0;
            movement.y = 0;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TabOpen();
        }
    }

    public void TabOpen() { 
        //Activation tablette

        if(SceneManager.GetActiveScene().name == "Introduction") return;
        
        if(!dialIsActive){
            tabIsActive = !tabIsActive;
            tablette.SetActive(tabIsActive);
            isActive = !tabIsActive;
        }

        if (tabIsActive)
        {
            btnTab.GetComponent<Animator>().SetBool("notif", false);
            isActive = false;
        }
        
        movementEnabled = !movementEnabled;
    }

    private void FixedUpdate()
    {
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.deltaTime;
        movement.Normalize(); // Pour s'assurer que le personnage se d�place � la m�me vitesse dans toutes les directions
        
        // if (walkableTilemap.HasTile(walkableTilemap.WorldToCell(newPosition)))
        // {
        //     rb.MovePosition(newPosition);
        // } 
        // else 
        // {
        //     Debug.Log("TRUC");
        // }
            
        rb.MovePosition(newPosition);
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
