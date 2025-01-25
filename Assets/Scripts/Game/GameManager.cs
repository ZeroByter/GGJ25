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
        public static UnityAction<int> OnAvailableBubblesChanged;

        public static GameManager Singleton { get; private set; }

        [SerializeField]
        private int bubblesPerTrash = 2;

        [SerializeField]
        private int maxLives = 5;

        public int maxBubbles = 20;
        public int bubblesAvailable = 7;

        public int score { get; private set; } = 0;
        public bool isGameRunning { get; private set; } = true;
        public int lives { get; private set; } = 3;
        public bool isGameLost { get; private set; } = false;

        private void Awake()
        {
            Singleton = this;
        }

        public void ChangeIsGameRunning(bool newIsGameRunning)
        {
            isGameRunning = newIsGameRunning;
            OnGameRunningChanged?.Invoke(newIsGameRunning);
        }

        public void SetGameLost()
        {
            isGameLost = true;
            ChangeIsGameRunning(false);
        }

        public void RemoveLife()
        {
            lives -= 1;

            if (lives <= 0)
            {
                SetGameLost();
            }

            OnLivesChanged?.Invoke(lives);

            OnTrashFailureChanged?.Invoke();
        }

        public void GiveLife()
        {
            lives = Mathf.Min(maxLives, lives + 1);

            OnLivesChanged?.Invoke(lives);
        }

        public void AddScore(int scoreToAdd)
        {
            score += scoreToAdd;

            OnScoreChanged?.Invoke(score);
        }

        public void AddBubble(int addCount)
        {
            var oldCount = bubblesAvailable;

            bubblesAvailable = Mathf.Min(maxBubbles, bubblesAvailable + addCount * bubblesPerTrash);

            if(bubblesAvailable != oldCount)
            {
                OnAvailableBubblesChanged?.Invoke(bubblesAvailable);

            }
        }

        public void RemoveBubble()
        {
            bubblesAvailable = Mathf.Max(0, bubblesAvailable - 1);

            if (bubblesAvailable == 0 && BubbleController.GetBubblesCount() == 0)
            {
                SetGameLost();
            }

            OnAvailableBubblesChanged?.Invoke(bubblesAvailable);
        }
    }
}
