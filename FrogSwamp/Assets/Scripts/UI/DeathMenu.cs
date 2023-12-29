using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private LevelLoader _levelLoader;
    
    [SerializeField] private Canvas _deathMenu;

    [SerializeField] private GameObject player;
    private PlayerHealth _playerHealth;
    
    [Inject] protected PlayerMovementScript playerMovementScript;
    
    [Inject] protected AudioManager _audioManager;
    

    private void Start()
    {
        
        _playerHealth = player.GetComponent<PlayerHealth>();

        if (!_playerHealth)
        {
            _playerHealth = playerMovementScript.transform.gameObject.GetComponent<PlayerHealth>();    
        }
        
        _deathMenu = GetComponent<Canvas>();
        _deathMenu.enabled = false;

    }

    private void Update()
    {
        if (_playerHealth.GetHealth() <= 0f)
        {
            _deathMenu.enabled = true;
        }
    }


    public void Restart()
    {
        _audioManager.Play("ButtonSound");
        _levelLoader.LoadLevel(SceneManager.GetActiveScene().name);
        _deathMenu.enabled = false;
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
