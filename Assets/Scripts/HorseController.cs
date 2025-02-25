using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HorseController : MonoBehaviour
{
    public float xPos, yPos;

    public int iPos, jPos;

    public BoardController boardControl;
    public GameController gameControl;
    public StepBack stepBackControl;
    private RectTransform pos;

    public RectTransform[] controlButtonsArr;

    private void Start()
    {
        pos = gameObject.GetComponent<RectTransform>();
    }

    public void Run(int buttonID)
    {
        int nextMoveI = 0, nextMoveJ = 0;

        switch (buttonID)
        {
            case 0:
                nextMoveI = -2;
                nextMoveJ = 1;
                break;

            case 1:
                nextMoveI = -1;
                nextMoveJ = 2;
                break;

            case 2:
                nextMoveI = 1;
                nextMoveJ = 2;
                break;

            case 3:
                nextMoveI = 2;
                nextMoveJ = 1;
                break;

            case 4:
                nextMoveI = 2;
                nextMoveJ = -1;
                break;

            case 5:
                nextMoveI = 1;
                nextMoveJ = -2;
                break;

            case 6:
                nextMoveI = -1;
                nextMoveJ = -2;
                break;

            case 7:
                nextMoveI = -2;
                nextMoveJ = -1;
                break;
        }

        if(iPos + nextMoveI < boardControl.size && jPos + nextMoveJ < boardControl.size && iPos + nextMoveI >= 0 && jPos + nextMoveJ >= 0 )
        {
            if (boardControl.CanMove(iPos + nextMoveI, jPos + nextMoveJ))
            {
                stepBackControl.SaveStep(xPos, yPos, iPos, jPos, boardControl.GetCageForPosition(iPos, jPos));

                boardControl.SelectCage(iPos, jPos);

                iPos += nextMoveI;
                jPos += nextMoveJ;

                yPos -= nextMoveI * boardControl.step;
                xPos += nextMoveJ * boardControl.step;

                pos.anchoredPosition = new Vector2(xPos, yPos);
                gameControl.PlusStep();

                CheckLooser();
            }
            else
            {
                Debug.Log("Уже выбрана");
            }
        }
        else
        {
            Debug.Log("Далеко");
        }
    }


    private void CheckLooser()
    {
        if (!boardControl.CanMove(iPos - 2, jPos + 1) && !boardControl.CanMove(iPos - 1, jPos + 2) &&
            !boardControl.CanMove(iPos + 1, jPos + 2) && !boardControl.CanMove(iPos + 2, jPos + 1) &&
            !boardControl.CanMove(iPos + 2, jPos - 1) && !boardControl.CanMove(iPos + 1, jPos - 2) &&
            !boardControl.CanMove(iPos - 1, jPos - 2) && !boardControl.CanMove(iPos - 2, jPos - 1))
        {
            gameControl.Looser();
        }
    }



    public void SetPosition(int iPos, int jPos)
    {
        this.iPos = iPos;
        this.jPos = jPos;

        xPos = boardControl.step / 2 + jPos * boardControl.step;
        yPos = -boardControl.step/ 2 - iPos * boardControl.step;

        pos.anchoredPosition = new Vector2(xPos, yPos);
    }

    public void SetButtonPositionAndSize()
    {
        for (int i = 0; i < controlButtonsArr.Length; i++)
        {
            controlButtonsArr[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, boardControl.step);
            controlButtonsArr[i].SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, boardControl.step);
        }

        controlButtonsArr[0].anchoredPosition = new Vector2(boardControl.step, 2 * boardControl.step);
        controlButtonsArr[1].anchoredPosition = new Vector2(2 * boardControl.step, boardControl.step);
        controlButtonsArr[2].anchoredPosition = new Vector2(2 * boardControl.step, -boardControl.step);
        controlButtonsArr[3].anchoredPosition = new Vector2(boardControl.step, -2 * boardControl.step);
        controlButtonsArr[4].anchoredPosition = new Vector2(-boardControl.step, -2 * boardControl.step);
        controlButtonsArr[5].anchoredPosition = new Vector2(-2 * boardControl.step, -boardControl.step);
        controlButtonsArr[6].anchoredPosition = new Vector2(-2 * boardControl.step, boardControl.step);
        controlButtonsArr[7].anchoredPosition = new Vector2(-boardControl.step, 2 * boardControl.step);

    }

    public void SetSize(float size)
    {
        pos.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size);
        pos.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
    }
}