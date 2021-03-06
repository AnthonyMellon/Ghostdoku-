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
    public GameObject background;
    private GameSettings gameSettings;

    //private int taps;
    void Start() {
        gameSettings = GameSettings.Instance;
        if (gameSettings.seenNarrative1)
        {
            GameObject.Find("Button").transform.localScale = new Vector3(0, 0, 0);
            Narrative1.SetActive(false);
        }
        if (gameSettings.seenNarrative2)
        {
            Narrative2.SetActive(false);
        }
        if(gameSettings.seenNarrative3 || gameSettings.seenNarrative4)
        {
            background.SetActive(false);
        }        
        //taps = 0;
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
        if(background == null)
        {
            background = GameObject.Find("Background");
        }

        if(gameSettings.seenNarrative4)
        {
            GameObject.Find("gravestonePlayable1").transform.GetChild(3).gameObject.SetActive(true);
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
            background.SetActive(false);
            GameObject.Find("Button").transform.localScale = new Vector3(0, 0, 0);            
        }
        else if (!gameSettings.seenNarrative3)
        {
            gameSettings.seenNarrative3 = true;
            Narrative3.SetActive(false);
            background.SetActive(false);
            GameObject.Find("Button").transform.localScale = new Vector3(0, 0, 0);
        }
        else if (!gameSettings.seenNarrative4)
        {
            gameSettings.seenNarrative4 = true;
            GameObject.Find("Button").transform.localScale = new Vector3(1, 1, 1);
            Narrative4.SetActive(false);
            background.SetActive(false);
            GameObject.Find("Button").SetActive(false);
            //GameObject.Find("Image").SetActive(false);            
            GameObject.Find("gravestonePlayable1").GetComponent<GraveInteraction>().triggered = true;
        }
        else
        {
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
