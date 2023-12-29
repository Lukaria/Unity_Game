using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class WinMenu : MonoBehaviour
{
    private static bool _gameIsWinned = false;
    
    [SerializeField] private Canvas _winMenu;
    
    [SerializeField] private LevelLoader _levelLoader;

    [Inject] private KillCounter _killCounter;
        
    [Inject] protected AudioManager _audioManager;
    

    private void Awake()
    {
        _winMenu = GetComponent<Canvas>();
        _winMenu.enabled = false;
    }

    public void WinGame()
    {
        _winMenu.enabled = true;
        _gameIsWinned = true;
    }

    public void RestartLevel()
    {
        _audioManager.Play("ButtonSound");
        _levelLoader.LoadLevel(SceneManager.GetActiveScene().name);
    }
    
    public void ExitToMainMenu()
    {
        _audioManager.Play("ButtonSound");
        _levelLoader.LoadLevel("MainMenuScene");
    }
    
    public void QuitGame()
    {
        _audioManager.Play("ButtonSound");
        Application.Quit();
    }
}
