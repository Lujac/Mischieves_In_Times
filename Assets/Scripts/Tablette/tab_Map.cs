using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tab_Map : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(string name)
    {
        if (name != SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene(name);
        }
    }
}
