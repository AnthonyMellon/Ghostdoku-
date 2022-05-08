using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class gridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler

{
    public GameObject number_text;
    private int number_ = 0;
    public GameObject gridBox;

    private bool selected_ = false;
    private int square_index_ = -1;
    private bool has_default_value_ = false;

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
        selected_ = false;
    }
    public void DisplayText()
    {
        if (number_ <= 0)
        {
            number_text.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        }

        else
        {
            number_text.GetComponent<TMPro.TextMeshProUGUI>().text = number_.ToString();
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
            //print($"Board is solved: {sudokuUtils.isSolved()}");
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
        
        Image image = gridBox.transform.Find("Image").GetComponent<Image>(); //The image component of the selected cell
        int myRow = sudokuUtils.getRow(sqaure_index); //The row of the selected cell
        int myCol = sudokuUtils.getCol(sqaure_index); //The col of the selected cell

        //If this is not the selected cell
        if (square_index_ != sqaure_index)
        {
            if(myRow == sudokuUtils.getRow(square_index_) || myCol == sudokuUtils.getCol(square_index_)) //If this cell is in the same row or col as the selected cell
            {
                image.color = new Color(1f, 1f, 0f); //Yellow
            }
            else
            {
                image.color = new Color(1f, 1f, 1f); //White
            }
            
            selected_ = false;
        } 
        //If this is the selected cell
        else
        {
            image.color = new Color(1f, 1f, 0.5f);//Gray
        }
    }
}
