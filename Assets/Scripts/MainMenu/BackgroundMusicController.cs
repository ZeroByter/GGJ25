using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ25.MainMenu
{
    public class BackgroundMusicController : MonoBehaviour
    {
        private static BackgroundMusicController Singleton;

        [SerializeField]
        private AudioSource gameMusic;
        [SerializeField]
        private AudioSource mainMenuMusic;
        [SerializeField]
        private float volumeLerpSpeed;

        private void Awake()
        {
            if(Singleton != null)
            {
                Destroy(gameObject);
                return;
            }

            Singleton = this;
            DontDestroyOnLoad(gameObject);

            gameMusic.volume = 0f;
            //mainMenuMusic.volume = 0f;
        }

        private void Update()
        {
            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                gameMusic.volume = Mathf.Lerp(gameMusic.volume, 0, volumeLerpSpeed * Time.unscaledDeltaTime);
            }
            else
            {
                gameMusic.volume = Mathf.Lerp(gameMusic.volume, 1f, volumeLerpSpeed * Time.unscaledDeltaTime);
            }
        }

        private void OnLevelWasLoaded(int level)
        {
            gameMusic.Stop();
            gameMusic.Play();
        }
    }
}
