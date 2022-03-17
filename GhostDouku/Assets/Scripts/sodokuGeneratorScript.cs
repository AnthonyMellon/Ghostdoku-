using UnityEngine;

public class sodokuGeneratorScript : MonoBehaviour
{
    private static int[] board;
    private static int numDigits;
    private static int sqrtNumDigits;
    public static int[] getSudoku(int numdigits, int numRemoved)
    {
        numDigits = numdigits;
        sqrtNumDigits = (int)Mathf.Sqrt(numDigits);
        board = new int[numDigits * numDigits];

        fillSudoku();

        return board;
    }

    private static void fillSudoku()
    {
        //Fill the 3 diagonal boxes
        for (int i = 0; i < sqrtNumDigits; i++)
        {
            fillBoxNoChecks(i + i * sqrtNumDigits);
        }

        //Fill the remaining boxes
        for (int i = 0; i < numDigits - sqrtNumDigits; i++)
        {
            int boxNum = Mathf.FloorToInt(i / sqrtNumDigits) + 1 + i;
            fillBoxWithChecks(boxNum);
        }

        //Remove X amount of spaces
    }

    private static void fillBoxNoChecks(int boxNum)
    {
        int[] boxContents = newNumSet();
        int index = 0;
        for (int i = 0; i < sqrtNumDigits; i++)
        {
            for (int j = 0; j < sqrtNumDigits; j++)
            {
                int boxIndex;
                int indexOffset;
                int trueIndex;

                //get the board index relative to the box (0 - numdigits)
                boxIndex = j + i * numDigits;

                //offset the board index based off box num
                indexOffset = (boxNum % sqrtNumDigits) * sqrtNumDigits; //Horizontal offset
                indexOffset += (Mathf.FloorToInt(boxNum / sqrtNumDigits)) * (numDigits * sqrtNumDigits); //Vertical offset

                trueIndex = boxIndex + indexOffset;

                board[trueIndex] = boxContents[index];
                index++;
            }
        }

    }

    private static void fillBoxWithChecks(int boxNum)
    {
        int[] boxContents = newNumSet();
        for (int i = 0; i < sqrtNumDigits; i++)
        {
            for (int j = 0; j < sqrtNumDigits; j++)
            {
                int boxIndex;
                int indexOffset;
                int trueIndex;

                //get the board index relative to the box (0 - numdigits)
                boxIndex = j + i * numDigits;

                //offset the board index based off box num
                indexOffset = (boxNum % sqrtNumDigits) * sqrtNumDigits; //Horizontal offset
                indexOffset += (Mathf.FloorToInt(boxNum / sqrtNumDigits)) * (numDigits * sqrtNumDigits); //Vertical offset

                trueIndex = boxIndex + indexOffset;
                //print(trueIndex);

                bool numPlaced = false;
                int row = Mathf.FloorToInt(trueIndex / numDigits);
                int col = trueIndex % numDigits;

                /* at the true index
                 * take the first number of the box contents
                 * check if its already in the row, or col, or is 0
                 * if no place it
                 * if yes try next number in box contents
                 */

                int count = 0;
                do
                {
                    int currentNum = boxContents[count];
                    if (!isInRow(currentNum, row) && !isInCol(currentNum, row) && currentNum != -1) //If the number wanting to be palced is legal
                    {
                        board[trueIndex] = currentNum;
                        boxContents[count] = -1;
                        numPlaced = true;
                    }
                    count++;

                } while (!numPlaced && count < numDigits);



                //do //Attempt to place a number in a cell
                //{
                //    print(k % 9);

                //    if (boxContents[k%9] != 0 && !isInRow(boxContents[k%9], row) && !isInCol(boxContents[k%9], col))
                //    {
                //        board[trueIndex] = boxContents[k];
                //        boxContents[k] = 0;
                //        numPlaced = true;
                //    }
                //    k++;

                //} while (!numPlaced && k < 100);
            }
        }
    }

    private static bool isInRow(int num, int row)
    {
        for (int i = 0; i < board.Length; i++) //Loop through the whole board
        {
            if (Mathf.FloorToInt(i / numDigits) == row && num == board[i])
            {
                return true;
            }
        }

        return false;
    }

    private static bool isInCol(int num, int col)
    {
        for (int i = 0; i < board.Length; i++) //Loop through the whole board
        {
            if (i % numDigits == col && num == board[i])
            {
                return true;
            }
        }

        return false;
    }

    private static int[] newNumSet()
    {
        int[] numSet = new int[numDigits];
        for (int i = 0; i < numDigits; i++)
        {
            numSet[i] = i + 1;
        }

        numSet = shuffleArray(numSet);

        return numSet;
    }

    private static int[] shuffleArray(int[] myArray)
    {
        //Fisher-Yates shuffle
        for (int i = myArray.Length - 1; i >= 0; i--)
        {
            int j = Random.Range(0, i);
            if (j != i)
            {
                int temp = myArray[j];
                myArray[j] = myArray[i];
                myArray[i] = temp;
            }
        }

        return myArray;
    }

    private void printSudoku()
    {
        string currentLine;
        int index = 0;
        for (int i = 0; i < numDigits; i++)
        {
            currentLine = "";
            for (int j = 0; j < numDigits; j++)
            {
                currentLine = $"{currentLine}{board[index]}";
                index++;
            }
            print(currentLine);
        }
    }

    private void Start()
    {
        getSudoku(9, 20);
        printSudoku();
    }

}
