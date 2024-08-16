using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TMP_Text _text;

    private void OnEnable()
    {
        _scoreCounter.Counted += OnScoreChanged;
    }

    private void OnDisable()
    {
        _scoreCounter.Counted -= OnScoreChanged;
    }

    private void OnScoreChanged(int score)
    {
        _text.text = score.ToString();
    }
}
