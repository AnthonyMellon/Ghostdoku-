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
        
    }

    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquarePos();
    }

    private void SpawnGridSquares()
    {
        for(int row = 0; row < rows; row++)
        {
            for(int column = 0; column < columns; column++)
            {
                gridSquares.Add(Instantiate(gridSquare) as GameObject);
                gridSquares[gridSquares.Count - 1].transform.parent = this.transform;
                gridSquares[gridSquares.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
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

        int[] gridNums = new int[rows * columns];
        numRemoved = 50;
        gridNums = sodokuGeneratorScript.getSudoku(columns, numRemoved);
        print(gridNums.Length);

        for (int j = 0; j < 81; j++)
        {
            gridSquares[j].GetComponent<gridSquare>().SetNumber(gridNums[j]);
        }
    }


}
