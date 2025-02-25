using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepBack : MonoBehaviour
{
    public int stepBackValue;

    public int steps;

    public List<float> backX;
    public List<float> backY;

    public List<int> backI;
    public List<int> backJ;

    public List<Cage> backCage;

    public HorseController horseControl;
    public BoardController boardControl;
    public GameController gameControl;

    public void SaveStep(float xPos, float yPos, int iPos, int jPos, Cage cage)
    {
        backX.Add(xPos);
        backY.Add(yPos);
        backI.Add(iPos);
        backJ.Add(jPos);
        backCage.Add(cage);

        steps++;

        if (stepBackValue > 0) stepBackValue--;
    }

    
    public void StepBackFunc()
    {
        if(steps > 0)
        {
            horseControl.SetPosition(backI[steps - 1], backJ[steps - 1]);
            backCage[steps - 1].StepBack();

            backX.RemoveAt(backX.Count - 1);
            backY.RemoveAt(backY.Count - 1);
            backI.RemoveAt(backI.Count - 1);
            backJ.RemoveAt(backJ.Count - 1);
            backCage.RemoveAt(backCage.Count - 1);

            steps--;
            gameControl.remaining++;
            gameControl.uiControl.UpdateStepAndRemainingText(gameControl.remaining);

            stepBackValue++;



            //////////////////
            ///

            gameControl.stopGame = false;
            gameControl.looserPanel.SetActive(false);
            gameControl.controlPanel.SetActive(true);

            gameControl.StopAllCoroutines();
            gameControl.StartCoroutine(gameControl.TimmerIE());


        }
    }
    
}
