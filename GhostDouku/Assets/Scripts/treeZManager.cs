using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeZManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            print("Organizing trees");
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y+50);
        }
    }
}
