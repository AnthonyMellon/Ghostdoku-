using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private static GameSettings instance;    

    public int previousRestorationLevel;
    public int restorationLevel;
    public static GameSettings Instance
    {
        get { return instance; }
    }

    void Awake() {
        if ( instance != null && instance != this )
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}