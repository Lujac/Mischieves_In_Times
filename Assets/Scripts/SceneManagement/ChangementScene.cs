using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementScene : MonoBehaviour
{
    [SerializeField]
    GameObject aAfficher;
    // Start is called before the first frame update
    void Start()
    {
        aAfficher.SetActive(false);
    }

    public void ChangeScene(string name)
    {
      PlayerControl.agentMovementInstance.SaveInfo();
        SceneManager.LoadScene(name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            aAfficher.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            aAfficher.SetActive(false);
        }
    }
}
