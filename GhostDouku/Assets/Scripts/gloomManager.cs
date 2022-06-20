using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gloomManager : MonoBehaviour
{
    public int maxRestoration;
    [Range(0, 10)]

    private GameSettings gameSettings;


    public Material fogMat;

    public

    // Start is called before the first frame update
    void Start()
    {
        gameSettings = GameSettings.Instance;
        Debug.Log(gameSettings.restorationLevel);
        // prevRestorationLevel = restorationLevel;

        //Layer trees properly
        // for(int i = 0; i < trees.transform.childCount; i++)
        // {
        //     Transform tree = trees.transform.GetChild(i);
        //     tree.position = new Vector3(tree.position.x, tree.position.y, tree.position.y + 50);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameSettings.restorationLevel != gameSettings.previousRestorationLevel)
        {
        Debug.Log(gameSettings.restorationLevel);
        gameSettings.previousRestorationLevel = gameSettings.restorationLevel;

        GameObject trees = GameObject.Find("trees");
        GameObject gravestone = GameObject.Find("gravestonePlayable1");
        GameObject grass = GameObject.Find("grass_tile_01");
        GameObject fog = GameObject.Find("Fog");
        GameObject Hut = GameObject.Find("Hut");
        GameObject flowers = GameObject.Find("Flowers");
        switch(gameSettings.restorationLevel)
        {
            case 1:
                //Show some living trees
                for(int i = 0; i < trees.transform.childCount; i+= 2)
                {
                    Transform tree = trees.transform.GetChild(i);
                    tree.Find("tree_dead").GetComponent<SpriteRenderer>().enabled = false;
                    tree.Find("tree_alive").GetComponent<SpriteRenderer>().enabled = true;
                }
                //Show some flowers
                for(int i = 0; i < flowers.transform.childCount; i+= 5)
                {
                    Transform flower = flowers.transform.GetChild(i);
                    flower.GetComponent<SpriteRenderer>().enabled = true;
                }
                

                //grave change || scuffed
                Transform grave1 = gravestone.transform.Find("gravestone_rubble_01");
                grave1.GetComponent<SpriteRenderer>().enabled = false;
                Transform grave2 = gravestone.transform.Find("gravestone_broken_decay_01");
                grave2.GetComponent<SpriteRenderer>().enabled = true;
                //ghost change
                Transform ghost1 = gravestone.transform.Find("ghost_idle_09_0");
                ghost1.GetComponent<SpriteRenderer>().enabled = true;
                Transform ghost2 = gravestone.transform.Find("ghost_idle_hidden_01_0");
                ghost2.GetComponent<SpriteRenderer>().enabled = false;

                //colour change
                grass.GetComponent<SpriteRenderer>().color = new Color32(237,214,255,255);
                fog.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,100);
            break;
            case 2:
                //Show more living trees
                for(int i = 0; i < trees.transform.childCount; i++)
                {
                    Transform tree = trees.transform.GetChild(i);
                    tree.Find("tree_dead").GetComponent<SpriteRenderer>().enabled = false;
                    tree.Find("tree_alive").GetComponent<SpriteRenderer>().enabled = true;
                }
                //Show more flowers
                for(int i = 0; i < flowers.transform.childCount; i++)
                {
                    Transform flower = flowers.transform.GetChild(i);
                    flower.GetComponent<SpriteRenderer>().enabled = true;
                }

                //grave change || scuffed
                Transform grave5 = gravestone.transform.Find("gravestone_rubble_01");
                grave5.GetComponent<SpriteRenderer>().enabled = false;
                Transform grave3 = gravestone.transform.Find("gravestone_broken_decay_01");
                grave3.GetComponent<SpriteRenderer>().enabled = false;
                Transform grave4 = gravestone.transform.Find("gravestone_01");
                grave4.GetComponent<SpriteRenderer>().enabled = true;
                //ghost change
                Transform ghost3 = gravestone.transform.Find("ghost_idle_09_0");
                ghost3.GetComponent<SpriteRenderer>().enabled = true;
                Transform ghost4 = gravestone.transform.Find("ghost_idle_hidden_01_0");
                ghost4.GetComponent<SpriteRenderer>().enabled = false;
                

                //hut change || scuffed
                Transform hut1 = Hut.transform.Find("Hut_Nice");
                hut1.GetComponent<SpriteRenderer>().enabled = true;
                Transform hut2 = Hut.transform.Find("Hut_Rundown");
                hut2.GetComponent<SpriteRenderer>().enabled = false;

                //color change
                grass.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,255);
                fog.GetComponent<SpriteRenderer>().color = new Color32(255,255,255,0);
            break;
        }   
        
        
        }
        // if(restorationLevel > maxRestoration)
        // {
        //     restorationLevel = maxRestoration;
        // }


        // if(restorationLevel != prevRestorationLevel) //Update the graveyard if the gloom level has changed since last update
        // {
        //     for(int i = 0; i < trees.transform.childCount; i++)
        //     {
        //         Transform tree = trees.transform.GetChild(i);


        //         if(Random.Range(0, maxRestoration) < restorationLevel)
        //         {
        //             tree.Find("tree_dead").GetComponent<SpriteRenderer>().enabled = false;
        //             tree.Find("tree_alive").GetComponent<SpriteRenderer>().enabled = true;
        //         }
        //         else
        //         {
        //             tree.Find("tree_dead").GetComponent<SpriteRenderer>().enabled = true;
        //             tree.Find("tree_alive").GetComponent<SpriteRenderer>().enabled = false;
        //         }

        //     }
        // }
        // prevRestorationLevel = restorationLevel;

        //Trees        
    }
}
