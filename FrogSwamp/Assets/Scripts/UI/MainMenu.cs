using System;
using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;

    [SerializeField] private GameObject _settingsMenu;
        
    [Inject] protected AudioManager _audioManager;
    private bool _isMusicPlayed = false;

    private void Awake()
    {
        _settingsMenu.SetActive(false);
        
    }

    private void FixedUpdate()
    {
        if (_isMusicPlayed) return;
        
        _audioManager.Play("MenuMusic");
        _isMusicPlayed = true;

    }

    public void ForestLevel()
    {
        _audioManager.Play("ButtonSound");
        _levelLoader.LoadLevel("Scenes/ForestScene");
    }
    
    public void DungeonLevel()
    {
        _audioManager.Play("ButtonSound");
        _levelLoader.LoadLevel("Scenes/DungeonScene");
    }
    
    public void Settings()
    {
        _audioManager.Play("ButtonSound");
        _settingsMenu.SetActive(true);
    }

    public void QuitGame()
    {
        _audioManager.Play("ButtonSound");
        Application.Quit();
    }
    
    
    public void DemoScene()
    {
        _levelLoader.LoadLevel("Scenes/DemoScene");
    }
}
