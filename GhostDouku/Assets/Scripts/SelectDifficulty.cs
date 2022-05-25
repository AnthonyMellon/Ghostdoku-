using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectDifficulty : MonoBehaviour
{
    [SerializeField] GameObject selectDif;

    public void Select()
    {
        selectDif.SetActive(true);
    }
    public void Exit()
    {
        selectDif.SetActive(false);
    }
    public void Easy()
    {

    }
    public void Medium()
    {

    }
    public void Hard()
    {

    }
}
