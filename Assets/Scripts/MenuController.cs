using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject levelPanel;

    public levelItem[] levelItems;

    public int page;

    public int maxPage;

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("onLevel"));

        if (!PlayerPrefs.HasKey("pageLevel")) PlayerPrefs.SetInt("pageLevel", 0);
        page = PlayerPrefs.GetInt("pageLevel");
    }

    public void OpenWindow(int windowId)
    {
        switch (windowId)
        {
            case 100:
                mainPanel.SetActive(true);
                levelPanel.SetActive(false);
                break;

            case 200:
                levelPanel.SetActive(true);
                mainPanel.SetActive(false);
                break;
        }
    }

    public void NextPageLevel(bool forward)
    {
        if (forward && page < maxPage) page++;
        else if (!forward && page > 0) page--;

        for (int i = 0; i < levelItems.Length; i++)
        {
            levelItems[i].levelNumber = (i + 1) + page * 9;
            levelItems[i].UpdateBtn();
        }
            
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("gameMap");
    }
}