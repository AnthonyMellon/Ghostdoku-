using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gloomManager : MonoBehaviour
{
    public int maxRestoration;
    [Range(0, 10)]
    public int restorationLevel;
    private int prevRestorationLevel;

    public Material fogMat;
    public GameObject trees;

    // Start is called before the first frame update
    void Start()
    {
        prevRestorationLevel = restorationLevel;

        //Layer trees properly
        for(int i = 0; i < trees.transform.childCount; i++)
        {
            Transform tree = trees.transform.GetChild(i);
            tree.position = new Vector3(tree.position.x, tree.position.y, tree.position.y + 50);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(restorationLevel > maxRestoration)
        {
            restorationLevel = maxRestoration;
        }


        if(restorationLevel != prevRestorationLevel) //Update the graveyard if the gloom level has changed since last update
        {
            for(int i = 0; i < trees.transform.childCount; i++)
            {
                Transform tree = trees.transform.GetChild(i);


                if(Random.Range(0, maxRestoration) < restorationLevel)
                {
                    tree.Find("tree_dead").GetComponent<SpriteRenderer>().enabled = false;
                    tree.Find("tree_alive").GetComponent<SpriteRenderer>().enabled = true;
                }
                else
                {
                    tree.Find("tree_dead").GetComponent<SpriteRenderer>().enabled = true;
                    tree.Find("tree_alive").GetComponent<SpriteRenderer>().enabled = false;
                }

            }
        }
        prevRestorationLevel = restorationLevel;

        //Trees        
    }
}
