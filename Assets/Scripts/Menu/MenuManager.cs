using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPrincipal;
    public GameObject selectionPersonnage;
    public GameObject saisieNom;
    public TMP_InputField inputFieldName;
    public GameObject personnageGarcon;
    public GameObject personnageFille;
    public CharacterAppearance characterAppearance;
    public TextMeshProUGUI messageText;
    public string scene;

    private string selectedSpriteName = "";

    private void Start()
    {
        menuPrincipal.SetActive(true);
        selectionPersonnage.SetActive(false);
        saisieNom.SetActive(false);
    }

    public void Commencer()
    {
        menuPrincipal.SetActive(false);
        selectionPersonnage.SetActive(true);
    }

    public void RetourSelectionPersonnage()
    {
        selectionPersonnage.SetActive(false);
        menuPrincipal.SetActive(true);
    }

    public void SelectionnerGarcon()
    {
        personnageGarcon.SetActive(true);
        personnageFille.SetActive(false);
        saisieNom.SetActive(true);
        selectionPersonnage.SetActive(false);

        // Mettre � jour le nom du sprite s�lectionn� pour le personnage gar�on
        selectedSpriteName = "chara_02";
    }

    public void SelectionnerFille()
    {
        personnageGarcon.SetActive(false);
        personnageFille.SetActive(true);
        saisieNom.SetActive(true);
        selectionPersonnage.SetActive(false);

        // Mettre � jour le nom du sprite s�lectionn� pour le personnage fille
        selectedSpriteName = "chara_01";
    }

    public void RetourSaisieNom()
    {
        saisieNom.SetActive(false);
        selectionPersonnage.SetActive(true);
    }

    public void ValiderPersonnage()
    {
        string selectedCharacter = inputFieldName.text;

        // V�rifier si le nom du personnage contient au moins 2 caract�res
        if (selectedCharacter.Length < 2)
        {
            messageText.text = "Ce nom est trop court !";
            return;
        }

        // V�rifier si le nom du personnage contient des mots interdits
        string[] bannedWords = {
        "Fran�ais",
        "Anglais",
        "bite", "kiki", "zizi", "batard", "teub", "p�nis", "nouille",
        "vagin", "chatte", "choune", "ut�rus", "clitoris", "clito",
        "cul", "cu", "anus", "anu", "trou", "balle", "tdb", "TDB",
        "nichon", "nichons", "sein", "seins", "poitrine",
        "couille", "couilles", "testicule", "testicules",
        "pute", "salope", "salop", "conne", "connard", "salaud", "PD", "con", "encule",
        "dick", "penis", "cock",
        "vagina", "pussy", "pusy", "pussi", "pusi", "uterus",
        "ass", "hole", "asshole", "ashole", "asole", "assol", "asol",
        "boob", "boobs", "boobies", "booby", "boobi", "boub", "boubs",
        "ball", "balls",
        "motherfucker", "mother", "fucker", "fuker", "sucker",
        "bitch", "biatch", "whore", "hore", "hor", "gay", "homo", "sex", "sexual", "cunt"
    };

        foreach (string word in bannedWords)
        {
            if (selectedCharacter.ToLower().Contains(word.ToLower()))
            {
                messageText.text = "Ce nom n'est pas autoris� !";
                return;
            }
        }

        // Enregistrer le personnage s�lectionn� dans les PlayerPrefs
        PlayerPrefs.SetString("SelectedCharacter", selectedCharacter);

        // Enregistrer le nom du sprite selectionne dans les PlayerPrefs
        PlayerPrefs.SetString("SelectedSpriteName", selectedSpriteName);

        // Charger une nouvelle scene qui contient votre joueur
        SceneManager.LoadScene(scene);
    }

    public void Quitter()
    {
        Application.Quit();
    }
}
