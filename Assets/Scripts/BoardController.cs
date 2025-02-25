using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class BoardController : MonoBehaviour
{
    public int size;
    public int screenSize;

    public float step;

    public GameObject[] cagesGO;
    public Cage[,] cages = new Cage[12, 12];

    public int[,] cagesEnabled = new int[12, 12];

    public GameController gameControl;
    public boardMap map;
    public GameObject boardInCanvas;
    public HorseController horse;
    public GameObject cagePref;

    public MapsContainer maspArr;

    private void Start()
    {
        size = DetectedMapSize();

        step = screenSize / size;

        gameControl.remaining = size * size;
        boardInCanvas.GetComponent<GridLayoutGroup>().cellSize = new Vector2(step, step);

        //  Спавним все клетки
        for (int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                Instantiate(cagePref, boardInCanvas.transform);
            }
        }

        cagesGO = GameObject.FindGameObjectsWithTag("cage");

        LoadMapInBoardMap();

        //  Перекидываем клетки из cagesGO в cages[]
        //  Выделяем их ориентируясь на матрицу
        //  Задаём им позицию
        int ident = 0;
        for (int i = 0; i < size; i++)
        {
            for(int j = 0; j < size; j++)
            {
                cages[i, j] = cagesGO[ident].GetComponent<Cage>();

                ident++;

                if (cagesEnabled[i, j] == 1)
                {
                    cages[i, j].outGame = true;
                    gameControl.remaining--;
                }
                else cages[i, j].outGame = false;
                cages[i, j].iPos = i;
                cages[i, j].jPos = j;
                cages[i, j].SetXY((step / 2) + j * step, (-step / 2) - i * step);
            }
        }

        //  Выбираем клеткам спрайты
        bool top = true, right = true, down = true, left = true;
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {

                if (i > 0) top = cages[i - 1, j].outGame;

                if (i < size - 1) down = cages[i + 1, j].outGame;

                if (j < size - 1) right = cages[i, j + 1].outGame;

                if (j > 0) left = cages[i, j - 1].outGame;

                cages[i, j].SelectSprite(top, right, down, left);
            }
        }

        gameControl.remaining--;
        gameControl.uiControl.UpdateStepAndRemainingText(gameControl.remaining);

        horse.SetSize(step);
        horse.SetButtonPositionAndSize();
    }

    private void LoadMapInBoardMap()
    {
        int[,] arr = maspArr.ReturnMap(gameControl.levelNumber);

        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                cagesEnabled[i, j] = arr[i, j];
                if (cagesEnabled[i, j] == 2) horse.SetPosition(i, j);
            }
        }
    }

    private int DetectedMapSize()
    {
        return maspArr.ReturnMap(gameControl.levelNumber).GetLength(1);
    }

    public void SelectCage(int iPos, int jPos)
    {
        cages[iPos, jPos].SelectCage();
    }

    public bool CanMove(int iPos, int jPos)
    {
        if(iPos >= size || jPos >= size || iPos < 0 || jPos < 0) return false;
        else if (cages[iPos, jPos].select || cages[iPos, jPos].outGame) return false;
        else return true;
    }

    public Cage GetCageForPosition(int iPos, int jPos)
    {
        return cages[iPos, jPos];
    }

    public bool GetCageForCageSprite(int iPos, int jPos)
    {
        Debug.Log(iPos + " | " + jPos);
        return cages[iPos, jPos].outGame;
    }
}
