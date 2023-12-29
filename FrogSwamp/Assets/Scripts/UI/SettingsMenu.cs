using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private Resolution[] _resolutions;
    
    
    [Inject] protected AudioManager _audioManager;

    void Start()
    {
        _resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; ++i)
        {
            options.Add(_resolutions[i].width + "x" + _resolutions[i].height);

            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        _audioManager.Play("ButtonSound");
        Screen.SetResolution(_resolutions[resolutionIndex].width, _resolutions[resolutionIndex].height, Screen.fullScreen);
    }
    
    public void SetVolume(float volume)
    {
        _audioManager.Play("ButtonSound");
        _audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        _audioManager.Play("ButtonSound");
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        _audioManager.Play("ButtonSound");
        Screen.fullScreen = isFullscreen;
    }

    public void Exit()
    {
        _audioManager.Play("ButtonSound");
        gameObject.SetActive(false);
    }
}
