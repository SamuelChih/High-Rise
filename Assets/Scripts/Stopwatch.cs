using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Stopwatch : MonoBehaviour
{
    private bool stopwatchActive = false;
    private float currentTime;
    public int startMinutes;
    public Text currentTimeText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopwatchActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimeText.text = time.ToString(@"mm\:ss\:fff");
    }

    public void StartStopwatch()
    {
        stopwatchActive = true;
    }

    public void StopStopwatch()
    {
        stopwatchActive = false;
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }
}
