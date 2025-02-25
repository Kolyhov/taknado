using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public Text timmerText;
    public Text remainingText;
    public Text levelText;

    public Image bcgImg;
    public Sprite[] bcgSprites;

    public GameController gameControl;

    private void Start()
    {
        bcgImg.sprite = bcgSprites[Random.Range(0, bcgSprites.Length)];
    }

    public void UpdateTimmerText(int timmer)
    {
        timmerText.text = timmer.ToString() + " s";
    }

    public void UpdateStepAndRemainingText(int remaining)
    {
        remainingText.text = remaining.ToString();
    }

    public void UpdateLevelText()
    {
        levelText.text = gameControl.levelNumber.ToString();
    }
}
