using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour{

    

    protected Animator anim; //gets animator to change anim on left right idle

    [System.NonSerialized]
    public GameObject gameManagerObject; //game manager in scene game
    [System.NonSerialized]
    public GameManager gameManager;
    [System.NonSerialized]
    public GameObject audioManagerGameObject;
    [System.NonSerialized]
    public AudioManagerGame audioManagerGame;

    protected SpriteRenderer sRenderer;

    [Header("stats")]
    public float speed;
    public int luck;
    public int health;
    public float cooldown;

    protected float currentCD;

    public GameObject ability;

    protected bool invis = false;

    protected bool invul = false;

    protected float maxX; //max X bound of screen

    protected float playerX; //used to calculate character not going out of bounds

    protected int curHealth;

    protected SpriteRenderer sprite; //Declare a SpriteRenderer variable to holds our SpriteRenderer component

    public void Awake()
    {
        speed = 5f;
        health = 3;
        luck = 1;
        cooldown = 6f;
        currentCD = 0;

        sprite = GetComponent<SpriteRenderer>(); //Set the reference to our SpriteRenderer component
        playerX = sprite.bounds.extents.x; //find length of sprite (from center to xbound)

        maxX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z)).x; //get maxX bound of screen

        anim = GetComponent<Animator>(); //gets animator to change anim on left right idle
        curHealth = health;

        gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        audioManagerGameObject = GameObject.Find("AudioManagerGame");
        audioManagerGame = audioManagerGameObject.GetComponent<AudioManagerGame>();
        sRenderer = GetComponent<SpriteRenderer>();

    }
    public void Start()
    {
        
    }

    private void Update()
    {
        if(currentCD > 0)
        {
            currentCD -= Time.deltaTime;
        }
        if (curHealth > 0)
        {
            if (Input.touchCount == 1) //if touching at one point
            {
                if (Input.GetTouch(0).phase != TouchPhase.Ended)
                {
                    float tempX = Input.GetTouch(0).position.x;
                    float tempY = Input.GetTouch(0).position.y;
                    if (tempY < Screen.height/5)
                    {
                        if (currentCD <= 0)
                        {
                            UseAbility();
                        }
                    }
                    else if (tempX < Screen.width / 2) //if left
                    {
                        if (transform.position.x > -maxX + playerX) //if position in bounds
                        {
                            sprite.flipX = true;
                            anim.SetTrigger("goWalk"); //trigger anim in time, repeat for right

                            transform.Translate(-speed * Time.deltaTime, 0, 0);
                        }
                    }
                    else if (tempX >= Screen.width / 2) //right
                    {
                        if (transform.position.x < maxX - playerX)
                        {
                            sprite.flipX = false;
                            anim.SetTrigger("goWalk");

                            transform.Translate(speed * Time.deltaTime, 0, 0);
                        }
                    }
                }
                else //dont move
                {
                    anim.SetTrigger("goIdle"); //if not moving set motion idle
                }
            }
            else //dont move
            {
                anim.SetTrigger("goIdle"); //if not moving set motion idle
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!invis)
        {
            
            if (collision.gameObject.tag == "Negative" && !invul)
            {
                audioManagerGame.Play("NegAudio");
                StartCoroutine("Flasher");
                curHealth--;
                gameManager.ReduceLife();
                if (curHealth <= 0)
                {
                    anim.SetTrigger("goDeath");
                    //Debug.Log("stop");
                    gameManager.StopScore();
                }
            }
            else if (collision.gameObject.tag == "Positive")
            {
                audioManagerGame.Play("PosAudio");
                gameManager.BonusIncrementScore();
            }
            else if (collision.gameObject.tag == "UmbrellaDrop")
            {
                gameManager.CreateUmbrella(transform.position);
            }
            else if (collision.gameObject.tag == "Invisible")
            {
                invis = true;
                GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 0.2f);
                Invoke("TurnOffInvis", 6f);
            } 
            
            Destroy(collision.gameObject);
        }
    }

    IEnumerator Flasher()
    {
        invul = true;
        for (int i = 0; i < 5; i++)
        {
            sRenderer.material.color = Color.clear;
            yield return new WaitForSeconds(.1f);
            sRenderer.material.color = Color.white;
            yield return new WaitForSeconds(.1f);
        }
        invul = false;
    }

    public void UseAbility()
    {

        Vector3 umbrellaPos = new Vector3(transform.position.x, transform.position.y + 2f);
        GameObject temp = Instantiate(ability, umbrellaPos, Quaternion.identity);
        Destroy(temp, 6f);
        currentCD += cooldown;
    }

    protected void TurnOffInvis()
    {
        invis = false;
        GetComponent<SpriteRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
    }
}
