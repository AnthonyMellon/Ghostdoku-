using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            VolumeLoad();
        }
        else
        {
            VolumeLoad();
        }
        DontDestroyOnLoad(transform.gameObject);     
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        VolumeSave();
    }

    public void VolumeLoad()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        AudioListener.volume = volumeSlider.value;
    }

    private void VolumeSave()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }

}
