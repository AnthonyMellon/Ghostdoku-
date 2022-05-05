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
            number_ = number;
            DisplayText();
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
        if(square_index_ != sqaure_index)
        {
            selected_ = false;
        }
        
    }
}
