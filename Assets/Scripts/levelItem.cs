using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelItem : MonoBehaviour
{
    public int levelNumber;
    public bool block;

    public Text levelNumberText;

    private MenuController menuControl;

    public int record;
    public Sprite[] recordSprites;
    public Image recordImage;
    public GameObject blockImg;

    private void Start()
    {
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Click);

        menuControl = GameObject.FindWithTag("MenuControl").GetComponent<MenuController>();

        UpdateBtn();
    }

    public void UpdateBtn()
    {
        levelNumberText.text = levelNumber.ToString();

        if (!PlayerPrefs.HasKey("record" + levelNumber.ToString())) PlayerPrefs.SetInt("record" + levelNumber.ToString(), 0);
        record = PlayerPrefs.GetInt("record" + levelNumber.ToString());

        recordImage.sprite = recordSprites[record];

        UpdateBlock();
    }

    public void UpdateBlock()
    {
        bool ansver = false;
        if (PlayerPrefs.GetInt("onLevel") + 1 < levelNumber) ansver = true;
        blockImg.SetActive(ansver);
        block = ansver;
    }

    private void Click()
    {
        if (!block)
        {
            PlayerPrefs.SetInt("LevelNumber", levelNumber);
            menuControl.StartLevel();
        }
        
    }
}
