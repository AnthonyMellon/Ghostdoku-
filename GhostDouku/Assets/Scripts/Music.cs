using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);     
    }


}
