using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChanger : MonoBehaviour
{
    private GameSettings gameSettings;
    public SpriteRenderer sprite;

    public Color color1;

    public Color color2;

    void Start()
    {
        gameSettings = GameSettings.Instance;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(gameSettings.restorationLevel != gameSettings.previousRestorationLevel)
        {
        gameSettings.previousRestorationLevel = gameSettings.restorationLevel;
        }

        switch(gameSettings.restorationLevel)
        {
            case 1:
                sprite.color = color1;
            break;
            case 2:
                sprite.color = color2;
            break;
        }
    }
}
