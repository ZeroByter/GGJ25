using Udar.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ25.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        private SceneField gameScene;

        public void StartPlaying()
        {
            SceneManager.LoadScene(gameScene.BuildIndex);
        }
    }
}
