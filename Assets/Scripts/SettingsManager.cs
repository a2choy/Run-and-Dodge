using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("SceneMenu"); //doesnt work in unity remote
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("SceneMenu"); 
    }
}
