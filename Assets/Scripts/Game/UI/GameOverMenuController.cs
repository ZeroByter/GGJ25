using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ25.Game.UI
{
    public class GameOverMenuController : MonoBehaviour
    {
        private LerpCanvasGroup lerpCanvasGroup;

        private void Awake()
        {
            lerpCanvasGroup = GetComponent<LerpCanvasGroup>();

            GameManager.OnGameRunningChanged += HandleGameRunningChanged;
        }

        private void HandleGameRunningChanged(bool gameRunning)
        {
            if(GameManager.Singleton.lives <= 0)
            {
                lerpCanvasGroup.SetAlpha(1f);
            }
        }

        public void TryAgain()
        {
            print("try again!");
            //SceneManager.LoadScene();
        }

        public void MainMenu()
        {
            print("main menu");
            //SceneManager.LoadScene();
        }

        private void OnDestroy()
        {
            GameManager.OnGameRunningChanged -= HandleGameRunningChanged;
        }
    }
}
