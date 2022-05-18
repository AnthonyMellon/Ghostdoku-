using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class gridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler

{
    Image image;
    Text text;
    public GameObject number_text;
    private int number_ = 0;
    public GameObject gridBox;

    private bool selected_ = false;
    private int square_index_ = -1;
    public bool has_default_value_ = false;

    public void SetHasDefaultValue(bool has_default)
    {
        has_default_value_ = has_default;
    }
    public bool GetHasDefaultValue()
    {
        return has_default_value_;
    }
    public bool IsSelected() { return selected_; }
    public void setSquareIndex(int index)
    {
        square_index_ = index;
    }
    public void Start()
    {
        image = gridBox.transform.Find("Image").GetComponent<Image>(); //The image component of the selected cell
        text = gridBox.transform.Find("Text").GetComponent<Text>();
        selected_ = false;
        if (has_default_value_)
        {
            image.color = new Color(0.35f, 0.35f, 0.35f, 1f); //Gray
            text.color = new Color(0.1f, 0.1f, 0.1f, 1f);
        }
        else
        {
            image.color = new Color(1f, 1f, 1f, 0.0f); //White
            text.color = new Color(0.46f, 0.78f, 0.73f, 1f);
        }
    }
    public void DisplayText()
    {
        if (number_ <= 0)
        {
            number_text.GetComponent<Text>().text = "";
        }

        else
        {
            number_text.GetComponent<Text>().text = number_.ToString();
        }


    }
    public void SetNumber(int number)
    {
        if(has_default_value_ == false)
        {
            //Update the array representing the sudoku board
            sudokuUtils.board[square_index_] = number;

            number_ = number;
            DisplayText();

            //Check if the sudoku has been solved
            print($"Board is solved: {sudokuUtils.isSolved()}");
            if(sudokuUtils.isSolved())
            {
                SceneManager.LoadScene("Hub");
            }
        }
           
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        selected_ = true;
        GameEvents.SquareSelectedMethod(square_index_);
    }
    public void OnSubmit(BaseEventData eventData)
    {

    }
    private void OnEnable()
    {
        GameEvents.OnUpdateSquareNumber += OnSetNumber;
        GameEvents.OnSquareSelected += OnSquareSelected;
    }
    private void OnDisable()
    {
        GameEvents.OnUpdateSquareNumber -= OnSetNumber;
        GameEvents.OnSquareSelected -= OnSquareSelected;
    }
    public void OnSetNumber(int number)
    {
        if (selected_)
        {
            SetNumber(number);
        }
    }
    public void OnSquareSelected(int sqaure_index)
    {       
        //If this is not the selected cell
        if (square_index_ != sqaure_index)
        {
            selected_ = false;
        }

        cellColours(sqaure_index);      
    }

    private void cellColours(int squareIndex)
    {
        int myRow = sudokuUtils.getRow(squareIndex); //The row of the selected cell
        int myCol = sudokuUtils.getCol(squareIndex); //The col of the selected cell

        bool defaultCell = transform.parent.Find($"Cell {squareIndex}").GetComponent<gridSquare>().has_default_value_;

        //Blank all the cells colors
        if (has_default_value_)
        {
            image.color = new Color(0.35f, 0.35f, 0.35f, 1f); //Gray
            text.color = new Color(0.1f, 0.1f, 0.1f, 1f);
        }
        else
        {
            image.color = new Color(1f, 1f, 1f, 0.0f); //White
            text.color = new Color(0.46f, 0.78f, 0.73f, 1f);
        }

        //If this is not the selected cell
        if (square_index_ != squareIndex)
        {
            if (!defaultCell)
            {
                if (myRow == sudokuUtils.getRow(square_index_) || myCol == sudokuUtils.getCol(square_index_)) //If this cell is in the same row or col as the selected cell
                {
                    //If there is a default value for this cell
                    if (has_default_value_)
                    {
                        image.color = new Color(0.8f, 0.8f, 0f, 0.5f); //Dark-Yellow    
                    }
                    else
                    {
                        image.color = new Color(1f, 1f, 0f, 0.5f); //Yellow    
                    }

                }
            }
        }

        //If this is the selected cell
        else
        {
            if (!defaultCell)
            {
                image.color = new Color(1f, 1f, 0.5f, 0.5f);//Pale-Yellow
            }
        }
    }
}
