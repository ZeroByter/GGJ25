using UnityEngine;
using UnityEngine.Events;

namespace GGJ25.Game
{
    public class GameManager : MonoBehaviour
    {
        public static UnityAction<int> OnScoreChanged;
        public static UnityAction<bool> OnGameRunningChanged;
        public static UnityAction<int> OnLivesChanged;
        public static UnityAction OnTrashFailureChanged;

        public static GameManager Singleton { get; private set; }

        public int score { get; private set; } = 0;
        public bool isGameRunning { get; private set; } = true;
        public int lives { get; private set; } = 3;

        private void Awake()
        {
            Singleton = this;
        }

        public void ChangeIsGameRunning(bool newIsGameRunning)
        {
            isGameRunning = newIsGameRunning;
            OnGameRunningChanged?.Invoke(newIsGameRunning);
        }

        public void RemoveLife()
        {
            lives -= 1;

            if (lives <= 0)
            {
                ChangeIsGameRunning(false);
            }

            OnLivesChanged?.Invoke(lives);

            OnTrashFailureChanged?.Invoke();
        }

        public void GiveLife()
        {
            lives += 1;

            OnLivesChanged?.Invoke(lives);
        }

        public void AddScore(int scoreToAdd)
        {
            score += scoreToAdd;

            OnScoreChanged?.Invoke(score);
        }
    }
}
