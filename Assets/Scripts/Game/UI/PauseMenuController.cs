using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ25.Game.UI
{
    public class PauseMenuController : MonoBehaviour
    {
        private LerpCanvasGroup lerpCanvasGroup;

        private bool isOpen = false;

        private void Awake()
        {
            lerpCanvasGroup = GetComponent<LerpCanvasGroup>();
        }

        public void Unpause()
        {
            SetIsOpen(false);
        }

        public void MainMenu()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
        }

        private void SetIsOpen(bool isOpen)
        {
            this.isOpen = isOpen;
            GameManager.Singleton.ChangeIsGameRunning(!isOpen);

            if (isOpen)
            {
                lerpCanvasGroup.SetAlpha(1f);
            }
            else
            {
                lerpCanvasGroup.SetAlpha(0f);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetIsOpen(!isOpen);
            }
        }
    }
}
