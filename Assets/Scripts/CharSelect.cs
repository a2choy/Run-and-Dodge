using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharSelect : MonoBehaviour
{
    private int selectedCharIndex = 0; //current char selected as a number

    [Header("List Of Characters")]
    [SerializeField] private List<CharacterSelectObject> characterList = new List<CharacterSelectObject>(); //list of all char

    [Header("UI References")]
    [SerializeField] private Text characterName;
    [SerializeField] private Image characterSplash; //can add more like select sound effect, animation etc

    [Header("Player Stats")]
    public Sprite healthSprite;
    public Sprite speedSprite;
    public Sprite luckSprite;
    public Image[] healthArray;
    public Image[] speedArray;
    public Image[] luckArray;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("lastChar")) //if no last played char 
        {
            PlayerPrefs.SetInt("lastChar", selectedCharIndex); //create at default 0
        }
        else
        {
            selectedCharIndex = PlayerPrefs.GetInt("lastChar"); //else show last played char
        }
        UpdateCharSelectUI();   
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }

    public void LeftButton()
    {
        selectedCharIndex--; //go left
        if(selectedCharIndex < 0) //if cant go left loop around, same for right
        {
            selectedCharIndex = characterList.Count - 1;
        }
        PlayerPrefs.SetInt("lastChar", selectedCharIndex); //set player pref, same for right
        UpdateCharSelectUI();
    }

    public void RightButton()
    {
        selectedCharIndex++;
        if (selectedCharIndex >= characterList.Count)
        {
            selectedCharIndex = 0;
        }
        PlayerPrefs.SetInt("lastChar", selectedCharIndex);
        UpdateCharSelectUI();
    }

    private void UpdateCharSelectUI()
    {
        characterSplash.sprite = characterList[selectedCharIndex].splash; //set sprite and text game object to array objects
        characterName.text = characterList[selectedCharIndex].characterName; //get name
        for(int i = 0; i < healthArray.Length; i++) //add hearts by looping through amount of heart that should be shown, repeat for speed
        {
            if(i < characterList[selectedCharIndex].health)
            {
                healthArray[i].enabled = true;
            }
            else
            {
                healthArray[i].enabled = false;
            }
        }
        for (int i = 0; i < speedArray.Length; i++)
        {
            if (i < characterList[selectedCharIndex].speed)
            {
                speedArray[i].enabled = true;
            }
            else
            {
                speedArray[i].enabled = false;
            }
        }
        for (int i = 0; i < luckArray.Length; i++)
        {
            if (i < characterList[selectedCharIndex].luck)
            {
                luckArray[i].enabled = true;
            }
            else
            {
                luckArray[i].enabled = false;
            }
        }
    }

    [System.Serializable]
    public class CharacterSelectObject //class that contains info
    {
        public Sprite splash;
        public int speed;
        public int health;
        public int luck;
        public string characterName;
    
    }
}
