using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpriteGlow;
using UnityEngine.SceneManagement;

public class GraveInteraction : MonoBehaviour
{
    public bool triggered = false;
    public GameObject narrative;

    private GameSettings gameSettings;

    void Start()
    {
        gameSettings  = GameSettings.Instance;
    }

    void Update() {
        
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            //print("I've been clicked!");
            if (hit.collider == this.GetComponent<BoxCollider2D>())
            {                
                if(gameSettings.restorationLevel == 0)
                {
                    if(!gameSettings.seenTut)
                    {
                        gameSettings.seenTut = true;
                        SceneManager.LoadScene("GhostScene");
                    }
                    else
                    {
                        SceneManager.LoadScene("Sudoku");
                    }                    
                }
                if(gameSettings.restorationLevel == 1)
                {
                    if(!gameSettings.seenGhostScene2)
                    {
                        gameSettings.seenGhostScene2 = true;
                        SceneManager.LoadScene("GhostScene2");
                    }
                    else
                    {
                        SceneManager.LoadScene("Sudoku");
                    }
                    
                }
                if(gameSettings.restorationLevel == 2)
                {
                    if (!gameSettings.seenGhostScene3)
                    {
                        gameSettings.seenGhostScene3 = true;
                        SceneManager.LoadScene("GhostScene3");
                    }
                    else
                    {
                        SceneManager.LoadScene("Sudoku");
                    }
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision) {
        transform.GetChild(0).GetComponent<SpriteGlowEffect>().OutlineWidth = 2;
        transform.GetChild(2).GetComponent<SpriteGlowEffect>().OutlineWidth = 2;
        transform.GetChild(3).gameObject.SetActive(true);
        if (!gameSettings.seenNarrative4)
        {
            GameObject.Find("Button").transform.localScale = new Vector3(1, 1, 1);
            narrative.GetComponent<Narrative>().Narrative4.SetActive(true);
            //GameObject.Find("ghost_idle_hidden_01_0").SetActive(true);
            Debug.Log("Enter");
            triggered = true;
        }
    }
    void OnTriggerExit2D(Collider2D other) {
        transform.GetChild(0).GetComponent<SpriteGlowEffect>().OutlineWidth = 0;
        transform.GetChild(2).GetComponent<SpriteGlowEffect>().OutlineWidth = 0;
        Debug.Log("Exit");
        triggered = false;
    }
}
