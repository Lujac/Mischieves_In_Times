using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue System/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public string[] dialogueLines; // Tableau de lignes de dialogue
    public string[] characterNames; // Tableau de noms des personnages

    public int GetLineCount(int index)
    {
        string line = dialogueLines[index];
        return line.Split('\n').Length;
    }
}


