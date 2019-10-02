using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    

    private float umbrellaActiveTime = 6f;

    public GameObject[] players = null; //player list add in ui
    GameObject player = null; //actual player thats selected
    PlayerBase script;

    [Header("Umbrella")]
    public GameObject umbrellaUse;

    [Header("GameObjects")]
    public GameObject gameOverPanel;
    public GameObject spawnerObject;
    public Text scoreNum;
    public Text highScoreNum;
    public Text scoreText;

    [Header("Heart")]
    public GameObject heart;
    

    private List<GameObject> lifePoints = new List<GameObject>();

    private float minX;
    private float maxY;
        
    [Header("Scripts")]
    public SpawnerScript spawner; //spawner script will use to stop spawnin on game over

    private int score; // current score

    private int n;
    private Vector3 pos;

    [Header("Misc")]
    public float startInterval = 1f;

    public static int luck;

    

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false); //set panel to not visible

        getChar();
        

        luck = script.luck;

        spawnerObject.SetActive(true);

        createHearts();

        

        Invoke("StartScore", 0); //start invoking points/time
        

    }

    private void createHearts()
    {
        float curX = minX; //current x for heart loc
        Vector3 heartPos = new Vector3(curX + 0.5f, maxY - 0.5f, 0);

        for (int i = 0; i < script.health; i++) // set health on screen
        {
            GameObject tempLife = Instantiate(heart, heartPos, Quaternion.identity);
            lifePoints.Add(tempLife);
            curX += 0.825f;
            heartPos = new Vector3(curX + 0.5f, maxY - 0.5f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void getChar()
    {
        n = PlayerPrefs.GetInt("lastChar"); //get lastchar/selectchar
        pos = new Vector3((float)-2.33, (float)-3.45, 0); //at this position spawn
        player = Instantiate(players[n], pos, Quaternion.identity); //instantiate player on board and remember player

        minX = -Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).x; //get mixX bound of screen
        maxY = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).y;

        switch (n)
        {
            case 0:
                script = (PlayerBase)player.GetComponent(typeof(PlayerBase)); //get script
                break;
            case 1:
                script = (PlayerCat)player.GetComponent(typeof(PlayerCat)); //get script
                break;
            default:
                break;
        }

    }

    public void CreateUmbrella(Vector3 playerPos)
    {
        Vector3 umbrellaPos = new Vector3(playerPos.x, playerPos.y + 2f);
        GameObject temp = Instantiate(umbrellaUse, umbrellaPos, Quaternion.identity);
        Destroy(temp, umbrellaActiveTime);
    }

    public void ReduceLife()
    {
        //Debug.Log("lifepoints 1: " + lifePoints.Count);
        if (lifePoints.Count > 0) //prevent IndexOutOfRangeException for empty list
        {
            GameObject temp = lifePoints[lifePoints.Count - 1];
            lifePoints.RemoveAt(lifePoints.Count - 1);
            Destroy(temp);
        }
        //Debug.Log("lifepoints 2: " + lifePoints.Count);
    }

    void IncrementScore()
    {

        score += UnityEngine.Random.Range(5, 10);
        scoreText.text = score.ToString();
    }

    public void BonusIncrementScore()
    {
        score += UnityEngine.Random.Range(250, 350); //add bonus points
        scoreText.text = score.ToString();
       
    }
 
    void StartScore()
    {
        InvokeRepeating("IncrementScore", 0, 0.1f); //start adding points per time
    }
    public void StopScore()
    {
        CancelInvoke("IncrementScore"); //stop score and add to highscore
        spawner.StopSpawning();
        if (PlayerPrefs.HasKey("HighScore"))
        {
            if(PlayerPrefs.GetInt("HighScore") < score)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", score);
        }

        highScoreNum.text = PlayerPrefs.GetInt("HighScore") + "";
        scoreNum.text = score + "";

        gameOverPanel.SetActive(true);

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("SceneMenu");
    }

    public void Restart()
    {
        gameOverPanel.SetActive(false);
        score = 0; 

        Destroy(player);
        player = Instantiate(players[n], pos, Quaternion.identity); //instantiate player on board and remember player

        float curX = minX; //current x for heart loc
        Vector3 heartPos = new Vector3(curX + 0.5f, maxY - 0.5f, 0);

        for (int i = 0; i < script.health; i++) // set health on screen
        {
            GameObject tempLife = Instantiate(heart, heartPos, Quaternion.identity);
            lifePoints.Add(tempLife);
            curX += 0.825f;
            heartPos = new Vector3(curX + 0.5f, maxY - 0.5f, 0);
        }

        Invoke("StartScore", 0); //start invoking points/time
        spawner.StartSpawning(startInterval);
    }
}
