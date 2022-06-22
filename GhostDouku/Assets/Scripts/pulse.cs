using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class pulse : MonoBehaviour
{
    // Start is called before the first frame update
    
    Bloom bloom;
    void Start()
    {
        Bloom tmp;
        Volume vol = GetComponent<Volume>();
        if(vol.profile.TryGet<Bloom>( out tmp ))
        {
            bloom = tmp;
        }
    }

    bool goingUp = true;
    // Update is called once per frame
    void Update()
    {
        
        if(bloom.intensity.value < .3f && goingUp)
            bloom.intensity.value += 0.0001f;
        else if(bloom.intensity.value > .3f && goingUp)
        {
            goingUp = false;
        }

        if(bloom.intensity.value > .1f && !goingUp)
            bloom.intensity.value -= 0.0001f;
        else if(bloom.intensity.value < .1f && !goingUp)
        {
            goingUp = true;
        }
    }
}
