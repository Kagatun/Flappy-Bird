using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private SpawnerEnemies _spawnerEnemies;
    [SerializeField] private StartScreen _startScreen;
    [SerializeField] private ExitScreen _exitScreen;
    [SerializeField] private ScoreCounter _scoreCounter;

    private void Awake()
    {
        _startScreen.Open();
    }

    private void OnEnable()
    {
        _startScreen.PlayButtonClicked += OnPlayButtonClick;
        _exitScreen.RestartButtonClicked += OnExitButtonClick;
    }

    private void OnDisable()
    {
        _startScreen.PlayButtonClicked -= OnPlayButtonClick;
        _exitScreen.RestartButtonClicked -= OnExitButtonClick;
    }

    private void OnPlayButtonClick()
    {
        _startScreen.Close();
        StartGame();
    }

    private void OnExitButtonClick()
    {
        _exitScreen.Close();
        StopGame();
    }

    private void StopGame()
    {
        Time.timeScale = 0f;
        _exitScreen.Open();
    }

    private void StartGame()
    {
        _spawnerEnemies.Reset();
        Time.timeScale = 1.0f;
        _spawnerEnemies.StartSpawnEnemy();
        _scoreCounter.Reset();
        _bird.Reset();
    }
}
