using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tilesPuzzle : MonoBehaviour
{
    public int[,] puzzleGrid;
    public object[,] hitBoxes;

    public int[] startCord = new int[2];
    public int[] endCord = new int[2];
    
    public bool isCorrect;
    void Start()
    {
        startGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        puzzleGrid = new int[4, 4];

        randomizeCords();

        for(int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                puzzleGrid[y, x] = 1;
            }
        }
        puzzleGrid[endCord[0], endCord[1]] = 0;
        puzzleGrid[startCord[0], startCord[1]] = 3;
    }
    private void randomizeCords()
    {
        for (int i = 0; i < startCord.Length; i++)
        {
            startCord[i] = Random.Range(0, 3);
            endCord[i] = Random.Range(0, 3);
        }
        if(startCord[0] == endCord[0] && startCord[1] == endCord[1])
        {
            randomizeCords();
        }
    }
    public void MovePiece(int cord1, int cord2)
    {

    }
}
