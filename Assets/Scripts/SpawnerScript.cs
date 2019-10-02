using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    private GameObject negative; //negative life object
    private GameObject positive; //plus point object

    public GameObject umbrella; //
    public GameObject invis; //
    public GameObject[] negativeArray; //holes possible game objects (like dogbone or fishbone, select one for use)
    public GameObject[] positiveArray;

    [Header("Speeds")]
    public int minBadThingsSpeed;
    public int maxBadThingsSpeed;
    public int minGoodThingsSpeed;
    public int maxGoodThingsSpeed;
    public int speedIncrement;

    private float maxX;
    private float minY;

    [Header("StartInterval")]
    public float startInterval = 0.1f;

    private int speedIncrementMultiplier = 0;


    [Header("LuckArrays")]
    public int[] negativeLuck = new int[5];
    public int[] positiveLuck = new int[5];
    public int[] umbrellaLuck = new int[5];
    //public int[] invisLuck = new int[5];

    public static int luck;

    // Start is called before the first frame update
    void Start()
    {
        
        
    }
    private void OnEnable()
    {
        int n = PlayerPrefs.GetInt("lastChar"); //get lastchar/selectchar
        maxX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).x; //get maxX bound of screen
        minY = -Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).y;
        negative = negativeArray[n]; //set gameobjects
        positive = positiveArray[n];

        luck = GameManager.luck;

        StartSpawning(startInterval);
    }

    public void luckMinus()
    {
        luck--;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartSpawning(float interval)
    {
        InvokeRepeating("SpawnObjects", 0.15f, interval);
    }
    public void PauseSpawning()
    {
        CancelInvoke("SpawnObjects");
    }
    public void StopSpawning()
    {
        CancelInvoke("SpawnObjects");


        //delete everything
        var positiveArr = GameObject.FindGameObjectsWithTag("Positive");

        foreach (GameObject item in positiveArr)
        {
            Destroy(item);
        }

        var negativeArr = GameObject.FindGameObjectsWithTag("Negative");

        foreach (GameObject item in negativeArr)
        {
            Destroy(item);
        }

        var invisibleArr = GameObject.FindGameObjectsWithTag("Invisible");

        foreach (GameObject item in invisibleArr)
        {
            Destroy(item);
        }

        var umbrellaArr = GameObject.FindGameObjectsWithTag("UmbrellaDrop");

        foreach (GameObject item in umbrellaArr)
        {
            Destroy(item);
        }
    }
    public void SpawnObjects()
    {
        float randLoc = Random.Range(-maxX, maxX); //gen random x
        Vector3 pos = new Vector3(randLoc, transform.position.y, 0); //gen vector at spawner location at random x loc
        int n = Random.Range(1, 1001); //random num to see which object is generated, 1-1000
        GameObject temp = null;
        float speed;
        if (n < negativeLuck[luck]) { //spawn negative at pos
            temp = Instantiate(negative, pos, Quaternion.identity);
            speed = (Random.Range(minBadThingsSpeed, maxBadThingsSpeed + 1) + (speedIncrementMultiplier * speedIncrement));
        }
        else if (n >= negativeLuck[luck] && n < positiveLuck[luck])
        {
            temp = Instantiate(positive, pos, Quaternion.identity);
            speed = (Random.Range(minGoodThingsSpeed, maxGoodThingsSpeed + 1) + (speedIncrementMultiplier * speedIncrement));
        }
        else if (n >= positiveLuck[luck] && n < umbrellaLuck[luck])
        {
            temp = Instantiate(umbrella, pos, Quaternion.identity);
            speed = (Random.Range(minGoodThingsSpeed, maxGoodThingsSpeed + 1) + (speedIncrementMultiplier * speedIncrement));
        }
        else
        {
            temp = Instantiate(invis, pos, Quaternion.identity);
            speed = (Random.Range(minGoodThingsSpeed, maxGoodThingsSpeed + 1) + (speedIncrementMultiplier * speedIncrement));
        }
         
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
    }
}
