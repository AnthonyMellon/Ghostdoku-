using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sodokuGeneratorScript : MonoBehaviour
{
    const int NUMROWS = 9;
    const int NUMCOLS = 9;
    static int[] board = new int[NUMROWS * NUMCOLS];

    private void Start()
    {
        generateNewSodoku();
    }
    public static int[] generateNewSodoku()
    {
        

        //Initialise the board array to all 0s       
        for(int i = 0; i < board.Length; i++)
        {
            board[i] = 0;
        }

        //Fill diagonal boxes

        //Fill rest of non diagonal boxes
            //For every cell, check each number 1-9 to make sure its safe to place

        //Once filled, remove x elements

        foreach(int i in board)
        {
            print(i);
        }
        return null;
    }

    private static int[] scatterArray(int[] myArray) //Randomise the order of elements in array
    {
        for(int i = 0; i < myArray.Length; i++)
        {
            int currMax = myArray.Length - 1 - i;
            int swapIndex = Random.Range(0, currMax);

            if(swapIndex != currMax)
            {
                int temp = myArray[currMax];
                myArray[currMax] = myArray[swapIndex];
                myArray[swapIndex] = temp;
            }
        }
        return myArray;
    }

    private static bool isInBox(int index, int num)
    {
        return false;
    }
    private static bool isInCol(int index, int num)
    {
        return false;
    }
    
    private static bool isInRow(int index, int num)
    {
        int startIndex = index - index % 9;
        for(int i = 0; i < 9; i++)
        {
            if (board[i + startIndex] == num) return true;
        }

        return false;
    }
}
