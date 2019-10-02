using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    public GameObject CharSelectPanel;
    // Start is called before the first frame update
    void Start()
    {
        CharSelectPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CharSelect()
    {
        CharSelectPanel.SetActive(true);
    }
    public void Play()
    {
        CharSelectPanel.SetActive(false);
        SceneManager.LoadScene("SceneGame");
        
    }
    public void Settings()
    {
        SceneManager.LoadScene("SceneSetting");
    }
    public void Finish()
    {
        Application.Quit();
    }
}
