using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public Transform playerTransform;
    public static double FitnessValue = 0;
    public static int sizeTargetValue = 0;
    public static int highscore = 0;
    public static int maximumScore = 0;
    public static int numberOfTotalCoins = 0;
    public static int numberOfColectedCoins = 0;
    public static int numberOfRemainingLife = 5;
    public static int playerFitness = 0;
    public static int numberOfBonusLife = 0;
    public static int numberOfInitialLife = 5;
    public int constantOfLife = 75;
    public int constantOfScore = 75;
    public int constantOfCoin = 25;
    public double constantOfCoinError = 0.95;

    public int scoreFunction = 0;

    public static double weightOfSpyke;
    public static double weightOfEagle;
    public static double weightOfFrog;
    public static double weightOfOpossum;
    public static double weightOfLife;

    //spyke, opossum, eagle, life, frog
    public static double[] weightOfElements = new double[5];

    private void Start()
    {
        cameraFollow.Setup(() => playerTransform.position);
    }

    public void setValues(double fV, int sV)
    {
        FitnessValue = fV;
        sizeTargetValue = sV;
    }

    public double getFitness()
    {
        return FitnessValue;
    }

    public int getSize()
    {
        return sizeTargetValue;
    }

    public void CalculatePlayerFitnessByDeath()
    {
        
        int highscoreValues = Mathf.RoundToInt(((float)highscore / maximumScore) * constantOfScore);
        Debug.Log("Valor da fitness AQUI >> : " + highscoreValues);
        Debug.Log("Maximum score: " + maximumScore);
        Debug.Log("HIGHSCORE: " + highscore);
        Debug.Log("Value of coins: " + CalculateCoinScore());
        playerFitness = highscoreValues + CalculateCoinScore();
        Debug.Log("PLAYER POR DEATH: " + playerFitness);
        //if()
        if (playerFitness >= 75)
        {
            //fitness deve manter igual
        }
        else
        {
            if(playerFitness < 75)
            {
                /*//fitness deve ser menor
                if(playerFitness < 20)
                {
                    FitnessValue = (double)((int)(FitnessValue * 0.8));
                    SetElementWeight(3, 2.5);
                }
                else
                {
                    FitnessValue = (double)((int)(FitnessValue * 0.9));
                    SetElementWeight(3, 2);
                }*/
                scoreFunction = Mathf.RoundToInt((playerFitness - 80) * 0.25f);
                FitnessValue += scoreFunction;
                if(FitnessValue <= 0)
                {
                    FitnessValue = 0;
                }
                SetSizeTarget();
                Debug.Log("Valores || Fitness: " + FitnessValue + " || playerFit: " + playerFitness + " || scoreFun: " + scoreFunction);
            }
        }
    }

    public void CalculatePlayerFitnessByScore()
    {
        Debug.Log("Vidas restantes 3: " + ((float)(numberOfRemainingLife / (float)(numberOfInitialLife + numberOfBonusLife))) * constantOfLife);
        int lifeValues = Mathf.RoundToInt(((float)(numberOfRemainingLife / (float)(numberOfInitialLife + numberOfBonusLife))) * constantOfLife);
        Debug.Log("LIFE VALUES: " + lifeValues + " || vidas restantes: " + numberOfRemainingLife + " || vidas coletadas: " + numberOfBonusLife + " || vidas iniciais: " + numberOfInitialLife);
        numberOfColectedCoins = 10;
        numberOfTotalCoins = 10;
        playerFitness = lifeValues + CalculateCoinScore();
        Debug.Log("PLAYER POR SCORE: " + playerFitness);

        
        if (playerFitness >= 80)
        {
            //fitness aumenta
            //FitnessValue = (double) ((int)(FitnessValue * 1.2));
            scoreFunction = Mathf.RoundToInt((playerFitness - 75) * 0.5f);
            FitnessValue += scoreFunction;
            if(FitnessValue >= 100)
            {
                FitnessValue = 100;
            }
        }
        else if(playerFitness < 80 && playerFitness > 40)
        {
            //fitness mantem
        }
        else if (playerFitness <= 40)
        {
            //fitness deve ser menor
            //FitnessValue = (double)((int)(FitnessValue * 0.9));
            scoreFunction = Mathf.RoundToInt((playerFitness - 45) * 0.5f);
            FitnessValue += scoreFunction;
            if(FitnessValue <= 0)
            {
                FitnessValue = 0;
            }
        }
        SetSizeTarget();
        Debug.Log("Valores || Fitness: " + FitnessValue + " || playerFit: " + playerFitness + " || scoreFun: " + scoreFunction);
    }

    private int CalculateCoinScore()
    {
        //Debug.Log("COINS COLECTED: " + numberOfColectedCoins);
         if(numberOfTotalCoins <= 0)
        {
            numberOfTotalCoins = 1;
        }
        int coinValues = Mathf.RoundToInt(((float)(numberOfColectedCoins / ((float)(numberOfTotalCoins * constantOfCoinError)))) * constantOfCoin);
        if(coinValues > constantOfCoin)
        {
            coinValues = constantOfCoin;
        }
        return coinValues;
    }

    private void SetElementWeight(int index, double weight)
    {
        weightOfElements[index] = weight;
    }

    private void SetSizeTarget()
    {
        sizeTargetValue = Mathf.RoundToInt((float)(FitnessValue / 2)) + 1;
    }
}
