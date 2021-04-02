using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] float defaultVolume = 0.8f;
    MusicPlayer musicPlayer;
    void Start()
    {
        volumeSlider.value = PlayerPrefsController.GetMasterVolume();

        musicPlayer = FindObjectOfType<MusicPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (musicPlayer)
        {
            musicPlayer.SetVolume(volumeSlider.value);
        }
        else return;
    }

    public void SaveSetting()
    {
        PlayerPrefsController.SetMasterVolume(volumeSlider.value);
    }
    public void SetDefaults()
    {
        volumeSlider.value = (defaultVolume);    
    }
}
