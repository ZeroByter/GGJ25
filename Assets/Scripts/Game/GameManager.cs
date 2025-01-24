using UnityEngine;
using UnityEngine.Events;

namespace GGJ25.Game
{
    public class GameManager : MonoBehaviour
    {
        public static UnityAction<bool> OnGameRunningChanged;
        public static UnityAction<int> OnLivesChanged;

        public static GameManager Singleton { get; private set; }

        public bool isGameRunning { get; private set; } = true;
        public int lives { get; private set; } = 3;

        private void Awake()
        {
            Singleton = this;
        }

        private void TriggerGameOver()
        {
            isGameRunning = false;
            OnGameRunningChanged?.Invoke(false);
        }

        public void RemoveLife()
        {
            lives -= 1;

            if(lives <= 0)
            {
                TriggerGameOver();
            }

            OnLivesChanged?.Invoke(lives);
        }
    }
}
