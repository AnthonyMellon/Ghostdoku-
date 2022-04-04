using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grid : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public int numRemoved = 2;
    public float everySquareOffset = 0.0f;
    public Vector2 startPos = new Vector2(0.0f, 0.0f);
    public GameObject gridSquare;
    public float squareScale = 1.0f;
    [Range(0, 100)]
    public int currentSudoku = 0;
    [Range(1, 3)]
    public int difficulty = 1;

    public TextAsset easySudokus;
    public TextAsset medSudokus;
    public TextAsset hardSudokus;

    private List<GameObject> gridSquares = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if (gridSquare.GetComponent<gridSquare>() == null)
            Debug.LogError("gridSquare object neds to have GridSquare script attatched");
        CreateGrid();
        SetGridNumbers();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetGridNumbers();
        }

    }

    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquarePos();
    }

    private void SpawnGridSquares()
    {
        int square_index = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                gridSquares.Add(Instantiate(gridSquare) as GameObject);
                gridSquares[gridSquares.Count - 1].GetComponent<gridSquare>().setSquareIndex(square_index);
                gridSquares[gridSquares.Count - 1].transform.parent = this.transform;
                gridSquares[gridSquares.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
                square_index++;
            }

        }
    }

    private void SetSquarePos()
    {
        var squareRect = gridSquares[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        offset.x = squareRect.rect.width * squareRect.transform.localScale.x + everySquareOffset;
        offset.y = squareRect.rect.height * squareRect.transform.localScale.y + everySquareOffset;

        int columnNum = 0;
        int rowNum = 0;
        foreach (GameObject square in gridSquares)
        {
            if (columnNum + 1 > columns)
            {
                rowNum++;
                columnNum = 0;
            }
            var posXOffset = offset.x * columnNum;
            var posYOffset = offset.y * rowNum;

            square.GetComponent<RectTransform>().anchoredPosition = new Vector3(startPos.x + posXOffset, startPos.y + posYOffset);
            columnNum++;
        }
    }
    private void SetGridNumbers()
    {
        //sodokuGeneratorScript.testPassRate(1000);


        numRemoved = 1;

        //gridNums = easySudokus.ToString().Split('\n')[currentSudoku].Split(',');

        string[] sudokuAsStrings = hardSudokus.ToString().Split('\n')[currentSudoku].Split(',');
        if (difficulty == 1)
        {
            sudokuAsStrings = easySudokus.ToString().Split('\n')[currentSudoku].Split(',');
        }
        else if (difficulty == 2)
        {
            sudokuAsStrings = medSudokus.ToString().Split('\n')[currentSudoku].Split(',');
        }
        else
        {
            sudokuAsStrings = hardSudokus.ToString().Split('\n')[currentSudoku].Split(',');
        }
        int[] gridNums = new int[sudokuAsStrings.Length];
        for (int i = 0; i < sudokuAsStrings.Length; i++)
        {
            gridNums[i] = System.Convert.ToInt16(sudokuAsStrings[i]);
        }

        //gridNums = sodokuGeneratorScript.getSudoku(columns, numRemoved);        

        for (int j = 0; j < 81; j++)
        {
            gridSquares[j].GetComponent<gridSquare>().SetNumber(gridNums[j]);
        }
    }




}
