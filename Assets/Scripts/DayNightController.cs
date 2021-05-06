using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    public static DayNightController current;

    private LevelTimer levelTimer;

    private void Awake()
    {
        levelTimer = FindObjectOfType<LevelTimer>();
        current = this;
    }

    public event Action IsDayTime;
    public event Action IsNightTime;
    public void NightTimeTrigger()
    {
        //Debug.Log("its nighttime");
        if (IsNightTime != null)
        {
            IsNightTime();
        }
    }
    public void DayTimeTrigger()
    {
        if (IsDayTime != null)
        {
            IsDayTime();
        }
    }

}
