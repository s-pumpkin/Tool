using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer
{
    private float timeEnd;
    private float currTime;

    private bool timeStop;
    private bool autoReset;

    private Action ComplyFunc;
    public Timer(float timeEnd, bool autoReset = true, TimeState timeState = TimeState.Update, Action ComplyFunc = null)
    {
        this.timeEnd = timeEnd;
        this.autoReset = autoReset;
        this.timeState = timeState;

        if (ComplyFunc != null)
            this.ComplyFunc += ComplyFunc;
    }

    public enum TimeState
    {
        Update,
        FixedUpdate,
    }
    private TimeState timeState;

    public void Update()
    {
        if (timeStop)
            return;

        currTime += UpdateFrequency();
        if (currTime >= timeEnd)
        {
            timeStop = true;
            if (autoReset)
                OnRestTime();

            ComplyFunc?.Invoke();
        }
    }

    public float GetCurrTime()
    {
        return currTime;
    }

    public void TimeOut()
    {
        timeStop = true;
    }

    public void TimeStart()
    {
        timeStop = true;
    }

    public void OnRestTime()
    {
        currTime = 0;
        timeStop = false;
    }

    public bool isTimeEnd()
    {
        return currTime >= timeEnd;
    }

    public void OtherNewRegisterComplyFunc(Action func)
    {
        ComplyFunc += func;
    }

    public void UnRegisterComplyFunc(Action func)
    {
        ComplyFunc -= func;
    }

    private float UpdateFrequency()
    {
        float time = 0;
        if (timeState == TimeState.Update)
            time = Time.deltaTime;
        else if (timeState == TimeState.FixedUpdate)
            time = Time.fixedDeltaTime;
        return time;
    }

}
