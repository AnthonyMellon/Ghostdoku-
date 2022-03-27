using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sodokuGeneratorScript : MonoBehaviour
{
    private static List<List<int>> board;
    private static int[] finalBoard;
    private static int numDigits;
    private static int sqrtNumDigits;




    public static int[] getSudoku(int numdigits, int numRemoved)
    {
        numDigits = numdigits;
        sqrtNumDigits = (int)Mathf.Sqrt(numDigits);
        board = new List<List<int>>();        
        finalBoard = new int[numdigits * numdigits];

        //Initialise the lists in board
        for(int i = 0; i < numdigits*numdigits; i++)
        {
            List<int> sublist = newNumSet();
            board.Add(sublist);
        }

        fillBoard();
        if (!boardIsValid()) getSudoku(numdigits, numRemoved);



        return finalBoard;
    }

    private static void fillBoard()
    {
        int index = 0;
        while(index < board.Count)
        {
            if(index < 0)
            {
                return;
            }

            //Are there available numbers for this cell?
            if (board[index].Count > 0) //Yes, there are available numbers
            {               
                if(!numConflicts(index, board[index][0])) //If first available number does not conflict
                {
                    finalBoard[index] = board[index][0]; //Use that the first available number
                    index++; //Advance 1 cell
                }
                else //If the first available number does conflict
                {
                    board[index].RemoveAt(0); //Remove from availabe numbers for this cell
                }
            }
            else //No, there arent available numbers
            {
                board[index] = newNumSet(); //Refresh this cells numbers
                index--; //Go back 1 cell
            }
        }

    }

    private static bool numConflicts(int index, int num)
    {
        if (isInRow(num, indexToRow(index)) || isInCol(num, indexToCol(index)) || isInBox(num, indexToBox(index))) return true;
        return false;
    }

    private static bool isInRow(int num, int row)
    {
        for (int i = 0; i < board.Count; i++) //Loop through the whole board
        {
            if (Mathf.FloorToInt(i / numDigits) == row && num == finalBoard[i])
            {
                return true;
            }
        }

        return false;
    }

    private static bool isInCol(int num, int col)
    {
        for (int i = 0; i < board.Count; i++) //Loop through the whole board
        {
            if (i % numDigits == col && num == finalBoard[i])
            {
                return true;
            }
        }

        return false;
    }

    private static bool isInBox(int num, int box)
    {
        for(int i = 0; i < numDigits; i++)
        {
            int index = Mathf.FloorToInt(i / sqrtNumDigits);
            index *= numDigits;
            index += i % sqrtNumDigits;
            index += (box % sqrtNumDigits) * sqrtNumDigits; //Horizontal offset
            index += (Mathf.FloorToInt(box / sqrtNumDigits)) * (numDigits * sqrtNumDigits); //Vertical offset

            if (finalBoard[index] == num)
            {
                return true;
            }
        }

        return false;
    }

    private static List<int> newNumSet()
    {
        List<int> numSet = new List<int>();
        for (int i = 0; i < numDigits; i++)
        {
            numSet.Add(i + 1);
        }

        numSet = shuffleList(numSet);

        return numSet;
    }

    private static List<int> shuffleList(List<int> myList)
    {
        //Fisher-Yates shuffle
        for (int i = myList.Count - 1; i >= 0; i--)
        {
            int j = Random.Range(0, i);
            if (j != i)
            {
                int temp = myList[j];
                myList[j] = myList[i];
                myList[i] = temp;
            }
        }

        return myList;
    }

    private static int indexToRow(int index)
    {
        return Mathf.FloorToInt(index / numDigits);
    }

    private static int indexToCol(int index)
    {
        return index % numDigits;
    }

    private static int indexToBox(int index)
    {        
        int boxCol = Mathf.FloorToInt(indexToCol(index) / sqrtNumDigits);
        int boxRow = Mathf.FloorToInt(indexToRow(index) / sqrtNumDigits) * sqrtNumDigits;
        return boxCol + boxRow;
    }

    private static bool boardIsValid()
    {
        foreach (int i in finalBoard)
        {
            if (i == 0) return false;
        }
        return true;
    }

    private static void printSudoku()
    {
        string currentLine;
        int index = 0;
        for (int i = 0; i < numDigits; i++)
        {
            currentLine = "";
            for (int j = 0; j < numDigits; j++)
            {
                currentLine = $"{currentLine}{finalBoard[index]}";
                index++;
            }
            print(currentLine);
        }
    }

    public static void testPassRate(int sampleSize)
    {
        int passes = 0;
        for(int i = 0; i < sampleSize; i++)
        {
            getSudoku(9, 20);
            if (boardIsValid()) passes++;
        }

        float passPercent = ((float)passes / sampleSize)*100;
        print($"");
        print($"Generated {sampleSize} Sudokos\n{passes} of {sampleSize} ({passPercent}%) generated successfuly");        
    }

}
