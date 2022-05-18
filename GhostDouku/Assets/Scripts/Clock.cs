using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Clock : MonoBehaviour
{
    int millisecond = 0;
    int minute = 0;
    int second = 0;

    private Text clock_;
    private float delta_time;
    private bool stop_clock = false;
    private static Clock instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(instance);
        }
        instance = this;

        clock_ = GetComponent<Text>();
        delta_time = 0;
    }
    void Start()
    {
        stop_clock = false;
    }

    void Update()
    {
        if(stop_clock == false)
        {
            delta_time += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(delta_time);
            string minute = LeadingZero(span.Minutes);
            string second = LeadingZero(span.Seconds);
            string millisecond = LeadingZero(span.Milliseconds);
            clock_.text = minute + ":" + second + ":" + millisecond;

        }
    }
    private string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
    
    public void onGameOver()
    {
        stop_clock = true;
    }
    private void onEnable()
    {
        GameEvents.OnGameOver += onGameOver;
    }
    private void onDisable()
    {
        GameEvents.OnGameOver -= onGameOver;
    }
}
