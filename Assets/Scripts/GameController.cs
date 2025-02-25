using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int levelNumber;

    public int remaining;

    public int timmer;

    public bool stopGame;

    public BoardController boardControl;
    public UiController uiControl;

    public GameObject controlPanel;
    public GameObject winPanel;
    public GameObject looserPanel;

    public Sprite[] medalSprites;
    public Image medalImage;
    public Sprite[] srasrStrites;
    public Image starsImage;

    private void Start()
    {
        levelNumber = PlayerPrefs.GetInt("LevelNumber");

        uiControl.UpdateLevelText();

        StartCoroutine(TimmerIE());
    }

    public IEnumerator TimmerIE()
    {
        while (!stopGame)
        {
            yield return new WaitForSeconds(1f);
            timmer++;

            uiControl.UpdateTimmerText(timmer);
        }
    }

    public void PlusStep()
    {
        remaining--;

        uiControl.UpdateStepAndRemainingText(remaining);
    }

    public void Looser()
    {
        if (remaining > 2)
        {
            stopGame = true;
            looserPanel.SetActive(true);
            controlPanel.SetActive(false);
        }
        else Winner();
    }

    public void Winner()
    {
        stopGame = true;
        winPanel.SetActive(true);
        controlPanel.SetActive(false);
        medalImage.sprite = medalSprites[remaining];
        starsImage.sprite = srasrStrites[remaining];

        if (PlayerPrefs.GetInt("record" + levelNumber.ToString()) < 3 - remaining)
            PlayerPrefs.SetInt("record" + levelNumber.ToString(), 3 - remaining);

        if (PlayerPrefs.GetInt("onLevel") < levelNumber) PlayerPrefs.SetInt("onLevel", levelNumber);
    }

    public void NextLevelButton()
    {
        if(levelNumber < 45) levelNumber++;
        PlayerPrefs.SetInt("LevelNumber", levelNumber);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TryAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Menu");
    }
}