using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NarrativeTrigger : MonoBehaviour
{
    private GameSettings gameSettings;
    public GameObject narrative;

    void Start()
    {
        gameSettings = GameSettings.Instance;
    }
        private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exiting");
        if (gameSettings.seenNarrative3)
        {
            narrative.GetComponent<Narrative>().Narrative3.SetActive(false);
        }
        else
        {
            narrative.GetComponent<Narrative>().Narrative3.SetActive(true);
        }
    }
}
