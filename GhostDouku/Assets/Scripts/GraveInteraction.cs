using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpriteGlow;
using UnityEngine.SceneManagement;

public class GraveInteraction : MonoBehaviour
{
    bool triggered = false;

    void Update() {
                if (Input.GetMouseButtonDown(0) && triggered)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider == this.GetComponent<BoxCollider2D>())
            {
            SceneManager.LoadScene("Sudoku");
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision) {
        transform.GetChild(0).GetComponent<SpriteGlowEffect>().OutlineWidth = 2;
        transform.GetChild(2).GetComponent<SpriteGlowEffect>().OutlineWidth = 2;
        Debug.Log("Enter");
        triggered = true;
    }
    void OnTriggerExit2D(Collider2D other) {
        transform.GetChild(0).GetComponent<SpriteGlowEffect>().OutlineWidth = 0;
        transform.GetChild(2).GetComponent<SpriteGlowEffect>().OutlineWidth = 0;
        Debug.Log("Exit");
        triggered = false;
    }
}
