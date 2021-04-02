using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    // Start is called before the first frame update
    [Tooltip("Our level timer in SECONDS")]
    [SerializeField] float levelTime = 10;
    bool triggeredLevelFinished = false;

    private void Update()
    {
        if (triggeredLevelFinished) { return; }
        GetComponent<Slider>().value = Time.timeSinceLevelLoad / levelTime;

        bool timeExpired = (Time.timeSinceLevelLoad >= levelTime);
        if (timeExpired)
        {
            triggeredLevelFinished = true;
            FindObjectOfType<LevelController>().LevelTimerFinished();
        }
    }
}
