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

        private LerpCanvasGroup lerpCanvasGroup;

        private void Awake()
        {
            lerpCanvasGroup = GetComponent<LerpCanvasGroup>();

            GameManager.OnGameRunningChanged += HandleGameRunningChanged;
            GameManager.OnScoreChanged += HandleScoreChanged;
        }

        private void HandleGameRunningChanged(bool gameRunning)
        {
            if(GameManager.Singleton.lives <= 0)
            {
                lerpCanvasGroup.SetAlpha(1f);
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
