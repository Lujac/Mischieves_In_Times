using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    GameObject inputFieldName, windDifficulty, windSexe, windName;

    private void Start()
    {
        windDifficulty.SetActive(true);
        windSexe.SetActive(false);
        windName.SetActive(false);
        inputFieldName.GetComponent<TMP_InputField>().onEndEdit.AddListener(SubmitName);

    }

    // difficulty 1 = college; 2 = lycée
    public void college()
    {
        PlayerPrefs.SetInt("difficulty", 1);
        windDifficulty.SetActive(false);
        windSexe.SetActive(true);
    }
    public void lycee()
    {
        PlayerPrefs.SetInt("difficulty", 2);
        windDifficulty.SetActive(false);
        windSexe.SetActive(true);
    }


    //sexe 1 = Boy; 2 = Girl
    public void thisIsABoy()
    {
        PlayerPrefs.SetInt("sexe", 1);
        windSexe.SetActive(false);
        windName.SetActive(true);
    }
    public void thisIsAGirl()
    {
        PlayerPrefs.SetInt("sexe", 2);
        windSexe.SetActive(false);
        windName.SetActive(true);
    }

    private void SubmitName(string name)
    {
        PlayerPrefs.SetInt("step", 0);
        PlayerPrefs.SetString("namePlayer", name);
        print("Difficulty = " + PlayerPrefs.GetInt("difficulty"));
        print("Sexe = " + PlayerPrefs.GetInt("sexe"));
        print("namePlayer = " + PlayerPrefs.GetString("namePlayer"));
        print("Step = " + PlayerPrefs.GetInt("step"));

    }
}
