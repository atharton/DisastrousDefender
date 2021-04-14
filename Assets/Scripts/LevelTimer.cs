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
    bool phase1Started = false;
    bool phase2Started = false;
    bool phase3Started = false;
    private void Awake()
    {
        slider = GetComponent<Slider>();
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
        if (!phase1Started)
        {
            DayNightController.current.DayTimeTrigger();
            phase1Started = true;
        }
        if (!phase2Started && slider.value >= 0.33f)
        {
            DayNightController.current.NightTimeTrigger();
            phase2Started = true;
        }
        if (!phase3Started && slider.value >= 0.67f)
        {
            DayNightController.current.DayTimeTrigger();
            phase3Started = true;
        }
    }

    public bool isNight()
    {
        if (slider.value >=0.33f && slider.value <= 0.67f) return true;
        else return false;
    }
}
