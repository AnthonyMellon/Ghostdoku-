using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class sodokuGeneratorScript : MonoBehaviour
{
    private static List<List<int>> board;
    private static int[] finalBoard;    
    private static int numDigits;
    private static int sqrtNumDigits;
    private static int clearAttempts = 0;
/*    private static string sudokuFilePath = "Assets/Sodokus/easy.txt";
    private static StreamWriter writer = new StreamWriter(sudokuFilePath, true);
    private static StreamReader reader = new StreamReader(sudokuFilePath);*/

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
        else
        {
            clearNCells(numRemoved);            
        }

/*        writer.WriteLine(finalBoard.ToString());
        writer.Close();*/

        return finalBoard;
    }

    private static void copyArray(int[] from, int[] to)
    {
        for(int i = 0; i < from.Length; i++)
        {
            to[i] = from[i];
        }
    }

    public static void clearNCells(int numToRemove)
    {
        int[] clearedBoard = new int[numDigits * numDigits];
        //print($"clearing {numToRemove} cells");
        copyArray(finalBoard, clearedBoard);
        int numRemoved = 0;
        while (numRemoved < numToRemove)
        {
            //print($"Removed {numRemoved} / {numToRemove}");

            int index;//Get a random index that hasnt already been cleared
            do
            {
                index = Random.Range(0, clearedBoard.Length);
            } while (clearedBoard[index] == 0);

            //print($"Clearing at Index{index}");
            clearedBoard[index] = 0;
            numRemoved++;
        }
        if (isSolvable(clearedBoard))
        {
            print($"Made a solvable puzzle with {numToRemove} empty spaces after {clearAttempts} attempts");
            copyArray(clearedBoard, finalBoard);
        }
        else
        {
            clearAttempts++;

            if (clearAttempts >= 1000)
            {
                print($"Could not make a solvable puzzle with with {numToRemove} empty spaces after {clearAttempts} attemps");
            }
            else
            {
                clearNCells(numToRemove);
            }
        }
    }
    
    public static bool isSolvable(int[] board)
    {
        int[] testBoard = new int[board.Length];
        copyArray(board, testBoard);

        List<int> emptyCells = new List<int>();

        //Find all the empty cells indexes       
        for(int i = 0; i < testBoard.Length; i++)
        {
            if (testBoard[i] == 0) emptyCells.Add(i);
        }

        int emptyCellIndex = 0; //The current cell being looked at
        bool solvable = true;
        do
        {
            //Find the possible numbers to fill the cell
            List<int> possibleNums = new List<int>();
            for(int i = 1; i <= numDigits; i++)
            {
                if (!numConflicts(emptyCells[emptyCellIndex], i, testBoard)) possibleNums.Add(i);
            }

            //If theres only 1 possible number to fill this cell
            if(possibleNums.Count == 1)
            {
                testBoard[emptyCells[emptyCellIndex]] = possibleNums[0]; //Insert number into cell
                emptyCells.RemoveAt(emptyCellIndex); //Remove the index from empty cells list
                emptyCellIndex = 0; //Start the loop over
            }

            //If theres more than 1 possible number to fill this cell
            else
            {
                //If this is the last empty cell
                if(emptyCellIndex == emptyCells.Count - 1) solvable = false; //Puzzle is not solvable

                //If this is not the last empty cell
                else emptyCellIndex++; //Move to next cell

            }

        } while (solvable && emptyCells.Count > 0);

        return solvable;
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
                if(!numConflicts(index, board[index][0], finalBoard)) //If first available number does not conflict
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

    private static bool numConflicts(int index, int num, int[] checkBoard)
    {
        if (isInRow(num, indexToRow(index), checkBoard) || isInCol(num, indexToCol(index), checkBoard) || isInBox(num, indexToBox(index), checkBoard)) return true;
        return false;
    }

    private static bool isInRow(int num, int row, int[] checkBoard)
    {
        for (int i = 0; i < board.Count; i++) //Loop through the whole board
        {
            if (Mathf.FloorToInt(i / numDigits) == row && num == checkBoard[i])
            {
                return true;
            }
        }

        return false;
    }

    private static bool isInCol(int num, int col, int[] checkBoard)
    {
        for (int i = 0; i < board.Count; i++) //Loop through the whole board
        {
            if (i % numDigits == col && num == checkBoard[i])
            {
                return true;
            }
        }

        return false;
    }

    private static bool isInBox(int num, int box, int[] checkBoard)
    {
        for(int i = 0; i < numDigits; i++)
        {
            int index = Mathf.FloorToInt(i / sqrtNumDigits);
            index *= numDigits;
            index += i % sqrtNumDigits;
            index += (box % sqrtNumDigits) * sqrtNumDigits; //Horizontal offset
            index += (Mathf.FloorToInt(box / sqrtNumDigits)) * (numDigits * sqrtNumDigits); //Vertical offset

            if (checkBoard[index] == num)
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


