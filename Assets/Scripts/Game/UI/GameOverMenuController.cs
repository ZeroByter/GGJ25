using TMPro;
using Udar.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ25.Game.UI
{
    public class GameOverMenuController : MonoBehaviour
    {
        [SerializeField]
        private SceneField mainMenuScene;
        [SerializeField]
        private TMP_Text scoreText;
        [SerializeField]
        private TMP_Text highScoreText;

        private LerpCanvasGroup lerpCanvasGroup;

        private void Awake()
        {
            lerpCanvasGroup = GetComponent<LerpCanvasGroup>();

            GameManager.OnGameRunningChanged += HandleGameRunningChanged;
            GameManager.OnScoreChanged += HandleScoreChanged;
        }

        private void HandleGameRunningChanged(bool gameRunning)
        {
            if(GameManager.Singleton.isGameLost)
            {
                lerpCanvasGroup.SetAlpha(1f);

                var highScore = PlayerPrefs.GetInt("HighScore");
                highScoreText.gameObject.SetActive(highScore > 0);
                highScoreText.text = $"High score: {highScore}";
            }
        }

        private void HandleScoreChanged(int newScore)
        {
            scoreText.text = $"Your final score: {newScore}";
        }

        public void TryAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void MainMenu()
        {
            SceneManager.LoadScene(mainMenuScene.BuildIndex);
        }

        private void OnDestroy()
        {
            GameManager.OnGameRunningChanged -= HandleGameRunningChanged;
            GameManager.OnScoreChanged -= HandleScoreChanged;
        }
    }
}
