using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsScript : MonoBehaviour {
    public GameObject settingsPanel;
    public Button settingsButton;

    public AudioClip toggleAudio;
    public Slider backgroundAudioSlider;
    public Slider soundFxAudioSlider;

    private bool isSettingsPanelActive = false;
    private AudioSource backgroundAudioSource;
    private GameObject backgroundAudio;

    private void Start()
    {
        backgroundAudio = GameObject.FindGameObjectWithTag("BackgroundSound");
        backgroundAudioSource = backgroundAudio.GetComponent<AudioSource>();
        
    }
    public void PlayToggleAudio()
    {
        AudioSource.PlayClipAtPoint(toggleAudio, Vector3.zero, GameVars.soundEffectVolume);
    }

    public void AdjustBackgroundAudioVolume()
    {
        backgroundAudioSource.volume = backgroundAudioSlider.value;
    }

    public void HandleBackgroundAudioSlider()
    {
        if (backgroundAudioSlider.enabled == true)
        {
            backgroundAudioSlider.enabled = false;
            backgroundAudioSource.volume = 0f;
        }
        else
        {
            backgroundAudioSlider.enabled = true;
            backgroundAudioSource.volume = backgroundAudioSlider.value;
        }
    }

    public void AdjustSoundEffectVolume()
    {
        GameVars.soundEffectVolume = soundFxAudioSlider.value;
    }

    public void HandleSoundEffectSlider()
    {
        if (soundFxAudioSlider.enabled == true)
        {
            soundFxAudioSlider.enabled = false;
            GameVars.soundEffectVolume = 0f;
        }
        else
        {
            soundFxAudioSlider.enabled = true;
            GameVars.soundEffectVolume = soundFxAudioSlider.value;
        }
    }

    public void HandleActiveSettingsPanel() {
        isSettingsPanelActive = !isSettingsPanelActive;
        settingsPanel.SetActive(isSettingsPanelActive);
        settingsButton.gameObject.SetActive(!isSettingsPanelActive);
    }

 

}
