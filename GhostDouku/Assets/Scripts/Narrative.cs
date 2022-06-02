using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Narrative : MonoBehaviour
{

    [SerializeField] GameObject Narrative1;
    [SerializeField] GameObject Narrative2;
    [SerializeField] GameObject Narrative3;
    [SerializeField] GameObject Narrative4;
    private int taps;
    void Start() {
        taps = 0;

    }

    public void Home(int sceneID)
    {
        taps++;
        Debug.Log(taps);
        switch(taps)
        {
            case 1:
            Narrative1.SetActive(false);
            break;
            case 2:
            Narrative2.SetActive(false);
            break;
            case 3:
            Narrative3.SetActive(false);
            break;
            default:
            SceneManager.LoadScene(sceneID);
            break;
        }
        
    }
}
