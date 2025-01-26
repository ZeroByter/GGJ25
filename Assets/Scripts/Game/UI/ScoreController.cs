using TMPro;
using UnityEngine;

namespace GGJ25.Game.UI
{
    public class ScoreController : MonoBehaviour
    {
        private TMP_Text scoreText;

        private void Awake()
        {
            scoreText = GetComponent<TMP_Text>();

            GameManager.OnScoreChanged += HandleScoreChanged;
        }

        private void HandleScoreChanged(int newScore)
        {
            var highScore = PlayerPrefs.GetInt("HighScore");

            if(highScore > 0)
            {
                scoreText.text = $"Score: {newScore} <color=yellow>({highScore})</color>";
            }
            else
            {
                scoreText.text = $"Score: {newScore}";
            }
        }

        private void OnDestroy()
        {
            GameManager.OnScoreChanged -= HandleScoreChanged;
        }
    }
}
