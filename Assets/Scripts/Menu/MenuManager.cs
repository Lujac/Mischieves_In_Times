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

        // Mettre à jour le nom du sprite sélectionné pour le personnage garçon
        selectedSpriteName = "chara_02";
    }

    public void SelectionnerFille()
    {
        personnageGarcon.SetActive(false);
        personnageFille.SetActive(true);
        saisieNom.SetActive(true);
        selectionPersonnage.SetActive(false);

        // Mettre à jour le nom du sprite sélectionné pour le personnage fille
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

        // Vérifier si le nom du personnage contient au moins 2 caractères
        if (selectedCharacter.Length < 2)
        {
            messageText.text = "Ce nom est trop court !";
            return;
        }

        // Vérifier si le nom du personnage contient des mots interdits
        string[] bannedWords = {
        "Français",
        "Anglais",
        "bite", "kiki", "zizi", "batard", "teub", "pénis", "nouille",
        "vagin", "chatte", "choune", "utérus", "clitoris", "clito",
        "cul", "cu", "anus", "anu", "trou", "balle", "tdb", "TDB",
        "nichon", "nichons", "sein", "seins", "poitrine",
        "couille", "couilles", "testicule", "testicules",
        "pute", "salope", "salop", "conne", "connard", "salaud", "PD", "con", "enculé",
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
                messageText.text = "Ce nom n'est pas autorisé !";
                return;
            }
        }

        // Enregistrer le personnage sélectionné dans les PlayerPrefs
        PlayerPrefs.SetString("SelectedCharacter", selectedCharacter);

        // Enregistrer le nom du sprite sélectionné dans les PlayerPrefs
        PlayerPrefs.SetString("SelectedSpriteName", selectedSpriteName);

        // Charger une nouvelle scène qui contient votre joueur
        SceneManager.LoadScene(scene);
    }

    public void Quitter()
    {
        Application.Quit();
    }
}
