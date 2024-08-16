using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private SpawnerEnemies _spawnerEnemies;
    [SerializeField] private SpawnerBulletEnemy _spawnerBulletEnemy;
    [SerializeField] private InputDetector _inputDetector;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private RestartScreen _exitScreen;

    private void Awake()
    {
        StopActive();
        _exitScreen.Close();
        _startScreen.Open();
    }

    private void OnEnable()
    {
        _bird.GameOver += StopGame;
        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _exitScreen.RestartButtonClicked += OnRestartButtonClick;
    }

    private void OnDisable()
    {
        _bird.GameOver -= StopGame;
        _startScreen.PlayButtonClicked -= OnPlayButtonClick;
        _exitScreen.RestartButtonClicked -= OnRestartButtonClick;
    }

    private void OnPlayButtonClick()
    {
        StartGame();
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void StopGame()
    {
        _exitScreen.Open();
        StopActive();
    }

    private void StartGame()
    {
        _startScreen.Close();
        _inputDetector.gameObject.SetActive(true);
        _spawnerBulletEnemy.gameObject.SetActive(true);
        _spawnerEnemies.gameObject.SetActive(true);
    }

    private void StopActive()
    {
        _inputDetector.gameObject.SetActive(false);
        _spawnerBulletEnemy.gameObject.SetActive(false);
        _spawnerEnemies.gameObject.SetActive(false);
    }
}
