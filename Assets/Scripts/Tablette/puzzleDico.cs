using System; 
using System.Collections.Generic; 

public static class puzzleDico
{
    // Référence statique (à ne pas instancier) de l'état de la complétion du puzzle Dico
    public static List<bool> listDefs = new List<bool>(){ false, false, false, false, false };

    public static void SetDefBool(int index) {
        listDefs[index - 1] = true;
    }

    // public static void SetDefAllTrue() {
    //     listDefs.ForEach(x => x = true);
    // }

    public static bool IsAllDefsTrue() {
        return listDefs.TrueForAll(x => x);
    }
}
