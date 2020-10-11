using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public bool start;

    public float time;
    public Text scoreLabel;

    private void Start()
    {
        scoreLabel.text = "Timer: 0:00";
    }

    private void Update()
    {
        if (start == true)
        {
            CalculateTime();
        }
    }

    void CalculateTime()
    {
        time += Time.deltaTime;
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        string timer = "Timer: " + minutes.ToString() + ":" + seconds.ToString().PadLeft(2, '0');
        scoreLabel.text = timer;
    }
}
