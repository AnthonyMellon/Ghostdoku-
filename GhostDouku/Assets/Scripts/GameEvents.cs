using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public delegate void updateSquareNumber(int number);
    public static event updateSquareNumber OnUpdateSquareNumber;
    
    public static void updateSquareNumberMethod(int number)
    {
        if(OnUpdateSquareNumber != null)
        {
            OnUpdateSquareNumber(number);
        }
    }
    public delegate void SquareSelected(int sqaure_index);
    public static event SquareSelected OnSquareSelected;
    public static void SquareSelectedMethod(int sqaure_index)
    {
        if (OnSquareSelected != null)
        {
            OnSquareSelected(sqaure_index);
        }
    }
}

