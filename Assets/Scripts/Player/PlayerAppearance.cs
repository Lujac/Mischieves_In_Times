// -----------------------------------------------------------------------------------------
// using classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// -----------------------------------------------------------------------------------------
// player movement class
public class PlayerAppearance : MonoBehaviour
{
    // static public members
    public static PlayerAppearance instance;

    // -----------------------------------------------------------------------------------------
    // public members
    public Transform tf;
    public Vector2 movement;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    // The name of the sprite sheet to use
    public string defaultSpriteSheetName;

    // -----------------------------------------------------------------------------------------
    // private members
    private Vector2 previousPosition;

    // The name of the currently loaded sprite sheet
    private string loadedSpriteSheetName;

    // The dictionary containing all the sliced up sprites in the sprite sheet
    private Dictionary<string, Sprite> spriteSheet;

    // -----------------------------------------------------------------------------------------
    // awake method to initialization
    void Awake()
    {
        instance = this;
        previousPosition = tf.position;
        this.LoadSpriteSheet();
        animator.SetFloat("speed", 0);
        animator.SetInteger("orientation", 4);
    }

    // -----------------------------------------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
    }

    // -----------------------------------------------------------------------------------------
    // fixed update method
    void FixedUpdate()
    {
        movement.x = tf.position.x - previousPosition.x;
        movement.y = tf.position.y - previousPosition.y;

        previousPosition = tf.position;

        animationUpdate();
    }

    // Runs after the animation has done its work
    private void LateUpdate()
    {
        // Check if the sprite sheet name has changed (possibly manually in the inspector)
        if (this.loadedSpriteSheetName != this.defaultSpriteSheetName)
        {
            // Load the new sprite sheet
            this.LoadSpriteSheet();
        }

        // Swap out the sprite to be rendered by its name
        // Important: The name of the sprite must be the same!
        this.spriteRenderer.sprite = this.spriteSheet[this.spriteRenderer.sprite.name];
    }

    // -----------------------------------------------------------------------------------------
    // Set the animation parameters
    public void animationUpdate()
    {
        animator.SetFloat("speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
        if (movement.x > 0 && Mathf.Abs(movement.y) < Mathf.Abs(movement.x))
            animator.SetInteger("orientation", 6);
        else if (movement.x < 0 && Mathf.Abs(movement.y) < Mathf.Abs(movement.x))
            animator.SetInteger("orientation", 2);
        else if (movement.y > 0)
            animator.SetInteger("orientation", 0);
        else if (movement.y < 0 || (movement.x == 0 && movement.y == 0))
            animator.SetInteger("orientation", 4);
    }

    // Loads the sprite from a sprite sheet based on the selected character
    private void LoadSpriteSheet()
    {
        // Get the selected sprite name from PlayerPrefs
        string selectedSpriteName = PlayerPrefs.GetString("SelectedSpriteName");

        // Set the sprite sheet folder based on the selected character
        string spriteSheetFolder = "Characters/" + selectedSpriteName + "/";

        // Load the sprites from a sprite sheet file (png).
        // Note: The file specified must exist in a folder named Resources
        string spriteSheetFilePath = spriteSheetFolder + "spritesheet";
        var sprites = Resources.LoadAll<Sprite>(spriteSheetFilePath);
        if (sprites.Length == 0)
        {
            // If the selected sprite sheet is not found, fall back to default sprite sheet
            spriteSheetFolder = "Characters/defaultCharacter/";
            spriteSheetFilePath = spriteSheetFolder + "spritesheet";
            sprites = Resources.LoadAll<Sprite>(spriteSheetFilePath);
        }

        this.spriteSheet = sprites.ToDictionary(x => x.name, x => x);

        // Remember the name of the sprite sheet in case it is changed later
        this.loadedSpriteSheetName = selectedSpriteName;
    }


}
