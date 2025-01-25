using Udar.SceneManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ25.MainMenu
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        private SceneField gameScene;
        [SerializeField]
        private MainMenuSpritesSpawner spritesSpawner;
        [SerializeField]
        private Collider2D floorCollider;

        [SerializeField]
        private GameObject mainMenuContainer;
        [SerializeField]
        private GameObject howToPlayContainer;

        private void Awake()
        {
            ShowMainMenu(true);
        }

        private void ShowMainMenu(bool show)
        {
            mainMenuContainer.SetActive(show);
            howToPlayContainer.SetActive(!show);
        }

        public void StartPlaying()
        {
            SceneManager.LoadScene(gameScene.BuildIndex);
        }

        public void ShowMainMenu()
        {
            floorCollider.enabled = true;
            spritesSpawner.ResetSpawnedSprites();
            ShowMainMenu(true);
        }

        public void ShowHowToPlay()
        {
            floorCollider.enabled = false;
            spritesSpawner.StopSpawningSprites();
            ShowMainMenu(false);
        }
    }
}
