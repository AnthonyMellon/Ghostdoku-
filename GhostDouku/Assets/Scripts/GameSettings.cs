using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private static GameSettings instance;    

    public int previousRestorationLevel;
    public int restorationLevel = 0;
    public bool seenNarrative1 = false;
    public bool seenNarrative2 = false;
    public bool seenNarrative3 = false;
    public bool seenNarrative4 = false;
    public bool seenTut = false;
    public bool seenGhostScene2 = false;
    public bool seenGhostScene3 = false;
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
