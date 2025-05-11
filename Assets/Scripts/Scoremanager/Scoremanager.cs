using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score instance;
    [SerializeField] private TextMeshProUGUI _currentScoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private Animator _highScoreAnimator; 
    private int _score;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
    _currentScoreText.text = _score.ToString();
     _highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        UpdateHighScore();
    }

    private void UpdateHighScore()
    {
        if (_score > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", _score);
            _highScoreText.text = _score.ToString();
            PlayHighScoreAnimation(); 
        }
    }

    private void PlayHighScoreAnimation()
    {
        if (_highScoreAnimator != null)
        {
            _highScoreAnimator.SetTrigger("HighScoreUpdated"); 
        }
    }

    public void UpdateScore()
    {
        _score += 50;
        _currentScoreText.text = _score.ToString();
        UpdateHighScore();
    }
}
