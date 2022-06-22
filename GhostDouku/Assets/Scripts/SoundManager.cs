using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    // Start is called before the first frame update
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
