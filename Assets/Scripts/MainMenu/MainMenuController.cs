using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ25.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        public void StartPlaying()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
        }
    }
}
