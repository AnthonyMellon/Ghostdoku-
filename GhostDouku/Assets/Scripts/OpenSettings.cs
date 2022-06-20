using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSettings : MonoBehaviour
{
    public GameObject Canvas;

    public void Toggle()
    {
        if(Canvas != null)
        {
            bool isActive = Canvas.activeSelf;
            Canvas.SetActive(!isActive);
        }
    }
}
