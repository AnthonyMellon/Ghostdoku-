using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class exitSudoku : MonoBehaviour
{
    public void goToHub()
    {
        print("I've been clicked!");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Hub");
    }    
}
