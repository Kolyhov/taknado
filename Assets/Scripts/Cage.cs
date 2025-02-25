using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class Cage : MonoBehaviour
{
    public bool select;
    public bool outGame;

    public float xPos, yPos;

    public int iPos, jPos;

    private Image myImage;

    private RectTransform pos;

    public GameObject selectSprite;

    //  0  - clear
    //  1  - 4 border
    //  2  - 3 border, top clear
    //  3  - 3 border, right clear
    //  4  - 3 border, down clear
    //  5  - 3 border, left clear
    //  6  - 2 border, top|right clear
    //  7  - 2 border, right|down clear
    //  8  - 2 border, down|left clear
    //  9  - 2 border, ltft|top clear
    //  10 - 1 border, top border
    //  11 - 1 border, right border
    //  12 - 1 border, down border
    //  13 - 1 border, left border
    public Sprite[] spritesArr;

    public bool blackCage;

    public void SelectCage()
    {
        if (!select)
        {
            select = true;
            selectSprite.SetActive(true);
        }
    }

    public void SetXY(float xPos, float yPos)
    {
        this.xPos = xPos;
        this.yPos = yPos;
    }

    public void StepBack()
    {
        selectSprite.SetActive(false);
        select = false;
    }

    public void SelectSprite(bool top, bool right, bool down, bool left)
    {
        myImage = gameObject.GetComponent<Image>();

        if (!outGame)
        {
            //  4 border
            if (top && right && down && left) myImage.sprite = spritesArr[1];

            //  3 border
            else if (!top && right && down && left) myImage.sprite = spritesArr[2];
            else if (top && !right && down && left) myImage.sprite = spritesArr[3];
            else if (top && right && !down && left) myImage.sprite = spritesArr[4];
            else if (top && right && down && !left) myImage.sprite = spritesArr[5];

            //  2 border
            else if (!top && !right && down && left) myImage.sprite = spritesArr[6];
            else if (top && !right && !down && left) myImage.sprite = spritesArr[7];
            else if (top && right && !down && !left) myImage.sprite = spritesArr[8];
            else if (!top && right && down && !left) myImage.sprite = spritesArr[9];
            else if (top && !right && down && !left) myImage.sprite = spritesArr[16];
            else if (!top && right && !down && left) myImage.sprite = spritesArr[15];

            //  1 border
            else if (top && !right && !down && !left) myImage.sprite = spritesArr[10];
            else if (!top && right && !down && !left) myImage.sprite = spritesArr[11];
            else if (!top && !right && down && !left) myImage.sprite = spritesArr[12];
            else if (!top && !right && !down && left) myImage.sprite = spritesArr[13];

            //  0 border
            else { myImage.sprite = spritesArr[0]; }
        }
        else myImage.sprite = spritesArr[14];

    }

    public void SetSize(float size)
    {
        pos = gameObject.GetComponent<RectTransform>();

        pos.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        pos.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
    }
}
