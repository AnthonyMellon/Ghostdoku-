
/****************************************
 * Sodoku Generator Script
 * Adapted from:
 * https://www.geeksforgeeks.org/program-sudoku-generator/
 ****************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sodokuGeneratorScript : MonoBehaviour
{
    private static int[,] mat;
    private static int N; //Number of ros / cols
    private static int SQRN; //Square root of N
    private static int K; //Number of missing digits
    
    private static void setUp()
    {
        //Get square root of N
        SQRN = (int)Mathf.Sqrt(N);

        mat = new int[N, N];
    }

    //Sodoku Generator
    private static void fillValues()
    {
        fillDiagonal();

        fillRemaining(0, SQRN);

        removeKDigits();
    }

    //Fill diagonal
    private static void fillDiagonal()
    {
        for(int i = 0; i <N; i += SQRN)
        {
            fillBox(i, i);
        }
    }

    //Returns false if given 3x3 block contains num
    private static bool unUsedInBox(int rowStart, int colStart, int num)
    {
        for(int i = 0; i < SQRN; i++)
        {
            for(int j = 0; j < SQRN; j++)
            {
                if (mat[rowStart + i, colStart + j] == num) return false;                    
            }
        }
        return true;
    }

    //Fill a 3x3 matrix
    private static void fillBox(int row, int col)
    {
        int num;
        for(int i = 0; i < SQRN; i++)
        {
            for (int j = 0; j < SQRN; j++)
            {
                do
                {
                    num = Random.Range(1, N + 1);

                } while (!unUsedInBox(row, col, num));
            }
        }
    }

    //Check if safe to put in cell
    private static bool checkIfSafe(int i, int j, int num)
    {
        return (unUsedInRow(i, num) &&
                unUsedInCol(j, num) &&
                unUsedInBox(i - i % SQRN, j - j % SQRN, num));
    }

    //Check in the row for existence
    private static bool unUsedInRow(int i, int num)
    {
        for(int j = 0; j < N; j++)
        {
            if(mat[i, j] == num)
            {
                return false;
            }
        }
        return true;
    }

    //Check in the col for existence
    private static bool unUsedInCol(int j, int num)
    {
        for(int i = 0; i < N; i++)
        {
            if(mat[i,j] == num)
            {
                return false;
            }
        }
        return true;
    }

    //A recursive function to fill remaining matrix
    private static bool fillRemaining(int i, int j)
    {
        if(j >= N && i < N - 1)
        {
            i = i + 1;
            j = 0;
        }
        if (i >= N && j >= N)
        {
            return true;
        }
        if (i < SQRN)
        {
            if (j < SQRN)
            {
                j = SQRN;
            }
        }
        else if (i < N - SQRN)
        {
            if (j == (int)(i / SQRN) * SQRN)
            {
                j += SQRN;
            }
        }
        else if (j == N - SQRN) 
        {
            i += 1;
            j = 0;
            if (i >= N)
            {
                return true;
            }
        }

        for (int num = 1; num <= N; num++)
        {
            if (checkIfSafe(i, j, num))
            {
                mat[i, j] = num;
                if(fillRemaining(i, j+1))
                {
                    return true;
                }
                mat[i, j] = 0;
            }
        }
        return false;
    }

    //Remove K digits to complete the game
    private static void removeKDigits()
    {
        int count = K;
        while (count != 0)
        {
            int cellID = Random.Range(0, N * N - 1);

            int i = cellID / N;
            int j = cellID % N;
            if (j != 0)
            {
                j -= 1;
            }

            if(mat[i,j] != 0)
            {
                count--;
                mat[i, j] = 0;
            }
        }
    }

    private static void printSodoku()
    {
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                print(mat[i, j]);
            }
        }
    }

    //Generate the sodoku
    public static int[,] getSodoku(int width, int numRemoved)
    {
        N = width;
        K = numRemoved;

        setUp();
        fillValues();
        //printSodoku();
        return mat;

    }

}
