using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("Our level timer in SECONDS")]
    [SerializeField] float levelTime = 30;
    private Slider slider;
    bool triggeredLevelFinished = false;
    [SerializeField] bool[] phases;
    int phaseLength;
    //bool phase1Started = false;
    //bool phase2Started = false;
    //bool phase3Started = false;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        phaseLength = phases.Length;
    }
    private void Update()
    {
        if (triggeredLevelFinished) { return; }
        slider.value = Time.timeSinceLevelLoad / levelTime;
        bool timeExpired = (Time.timeSinceLevelLoad >= levelTime);
        if (timeExpired)
        {
            triggeredLevelFinished = true;
            FindObjectOfType<LevelController>().LevelTimerFinished();
        }
        CheckDayNightCycle();

    }

    private void CheckDayNightCycle()
    {
        int i = 0;
        foreach (bool phase in phases)
        {
            //Debug.Log("here" + i);
            if (phases[phaseLength - 1] == true) return;
            if (!phase && slider.value >= (float)i / phaseLength)
            {
                if (i % 2 == 1) DayNightController.current.NightTimeTrigger();
                else DayNightController.current.DayTimeTrigger();
                phases[i] = true;
            }
            i++;
        }
    }
}
