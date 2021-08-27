using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TestTimer : MonoBehaviour
{
    public int timeEnd = 50;
    public float Currtime
    {
        get
        {
            return (int)timer.GetCurrTime();
        }
    }
    public bool AutoReset = true;
    Timer timer;

    public Text timeText;
    public Text endText;

    private void Start()
    {
        timer = new Timer(timeEnd, AutoReset, Timer.TimeState.Update, RunFunc);
    }

    public void Update()
    {
        timer.Update();
        timeText.text = Currtime.ToString();
    }

    public void RunFunc()
    {
        endText.text = "TimeEnd!!!";
        Debug.Log("TimeEnd!!!!!");
    }
}
