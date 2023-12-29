using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Zenject;

public class PauseMenu : MonoBehaviour
{
    private static bool _gameIsPaused = false;
    
    [SerializeField] private InputActionReference _pause;

    [SerializeField] private Canvas _pauseMenu;

    [SerializeField] private LevelLoader _levelLoader;
    
    [Inject] protected PlayerMovementScript playerMovementScript;
    
    [FormerlySerializedAs("_settingMenu")] [SerializeField] private GameObject _settingsMenu;

    [Inject] protected AudioManager _audioManager;

    private void Awake()
    {
        _pauseMenu = GetComponent<Canvas>();
        _pauseMenu.enabled = false;

        _pause.action.performed += PauseGame;
        _settingsMenu.SetActive(false);
    }

    
    void PauseGame(InputAction.CallbackContext context)
    {
        if(playerMovementScript.transform.gameObject.GetComponent<PlayerHealth>().GetHealth() <= 0)
            return;
        
        if (_gameIsPaused)
        {
            Resume();
        }
        else
        {
            _settingsMenu.SetActive(false);
            _audioManager.Play("ButtonSound");
            Pause();
        }
    }

    public void Resume()
    {
        _pauseMenu.enabled = false;
        Time.timeScale = 1f;
        _gameIsPaused = false;
    }

    public void Pause()
    {
        _pauseMenu.enabled = true;
        Time.timeScale = 0f;
        _gameIsPaused = true;
    }

    public void Settings()
    {
        _audioManager.Play("ButtonSound");
        _settingsMenu.SetActive(true);
    }
    
    public void ExitToMainMenu()
    {
        _audioManager.Play("ButtonSound");
        Time.timeScale = 1f;
        _levelLoader.LoadLevel("MainMenuScene");
    }
    
    public void QuitGame()
    {
        _audioManager.Play("ButtonSound");
        Application.Quit();
    }

    public static bool isGamePaused()
    {
        return _gameIsPaused;
    }
    
    public void OnDestroy()
    {
        _pause.action.performed -= PauseGame;
    }
}
