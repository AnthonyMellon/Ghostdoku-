using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gridSquare : Selectable
{
    public GameObject number_text;
    private int number_ = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayText()
    {
        if (number_ <= 0)
            number_text.GetComponent<Text>().text = "";
        else
            number_text.GetComponent<Text>().text = number_.ToString();
            
    }
    public void SetNumber(int number)
    {
        number_ = number;
        DisplayText();
    }
}
