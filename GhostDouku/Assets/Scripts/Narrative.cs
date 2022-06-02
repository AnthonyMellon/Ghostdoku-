using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Narrative : MonoBehaviour
{

    public GameObject Narrative1;
    public GameObject Narrative2;
    public GameObject Narrative3;
    public GameObject Narrative4;
    public GameObject player;
    private GameSettings gameSettings;

    //private int taps;
    void Start() {
        gameSettings = GameSettings.Instance;
        if (gameSettings.seenNarrative1)
        {
            Narrative1.SetActive(false);
        }
        if (gameSettings.seenNarrative2)
        {
            Narrative2.SetActive(false);
        }
        //taps = 0;
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

    }

    public void NarrativeClick()
    {
        if (!gameSettings.seenNarrative1)
        {
            gameSettings.seenNarrative1 = true;
            Narrative1.SetActive(false);
        }else if(!gameSettings.seenNarrative2)
        {
            gameSettings.seenNarrative2 = true;
            Narrative2.SetActive(false);
        }
        else if (!gameSettings.seenNarrative3)
        {
            gameSettings.seenNarrative3 = true;
            Narrative3.SetActive(false);
        }
        else if (!gameSettings.seenNarrative4)
        {
            gameSettings.seenNarrative4 = true;
            Narrative4.SetActive(false);
            GameObject.Find("gravestonePlayable1").GetComponent<GraveInteraction>().triggered = true;
        }
    }

    /*public void Home(int sceneID)
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
            player.GetComponent<PlayerMovement>().canMove = true;
            break;
            case 3:
            Narrative3.SetActive(false);
            break;
            default:
            Narrative4.SetActive(false);
                GameObject.Find("gravestonePlayable1").GetComponent<GraveInteraction>().triggered = true;
            break;
        }
        
    }*/
}
