using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

public class DayNightObject : MonoBehaviour
{
    [SerializeField] float dayLightIntensity;
    [SerializeField] float nightLightIntensity;
    [SerializeField] float fadeOutTime;

    private Light2D myLight2D;
    private void Awake()
    {
        DOTween.Init(true, true, LogBehaviour.Default);
        if (TryGetComponent<Light2D>(out Light2D light2D)) myLight2D = light2D;
    }
    private void Start()
    {
        DayNightController.current.IsDayTime += onDay;
        DayNightController.current.IsNightTime += onNight;
    }

    private void onNight()
    {
        //Debug.Log("I am called night");
        //Debug.Log(myLight2D.intensity);
        if (myLight2D != null) 
        {
            //myLight2D.intensity;
            float lightIntensity = myLight2D.intensity;
            var tweener = DOTween.To(
                () => lightIntensity, 
                x => lightIntensity = x, 
                nightLightIntensity, 
                fadeOutTime).OnUpdate(()=>myLight2D.intensity = lightIntensity);

            Debug.Log("I am called night222");
        }
    }
    private void onDay()
    {
        if (myLight2D != null)
        {
            //myLight2D.intensity;
            float lightIntensity = myLight2D.intensity;
            var tweener = DOTween.To(
                () => lightIntensity,
                x => lightIntensity = x,
                dayLightIntensity,
                fadeOutTime).OnUpdate(() => myLight2D.intensity = lightIntensity);

            Debug.Log("I am called night222");
        }
    }
    private void OnDestroy()
    {
        DayNightController.current.IsDayTime -= onDay;
        DayNightController.current.IsNightTime -= onNight;
    }

}
