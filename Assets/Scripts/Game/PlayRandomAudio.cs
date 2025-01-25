using System.Collections;
using UnityEngine;

namespace GGJ25.Game
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayRandomAudio : MonoBehaviour
    {
        [SerializeField]
        private AudioClip[] audios;
        [SerializeField]
        private bool destroyOnEnd;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private IEnumerator DestroyAfterPlay(float delay)
        {
            yield return new WaitForSeconds(delay);

            Destroy(gameObject);
        }

        public void Play()
        {
            audioSource.Stop();

            audioSource.clip = audios[Random.Range(0, audios.Length)];
            audioSource.Play();

            if (destroyOnEnd)
            {
                StartCoroutine(DestroyAfterPlay(audioSource.clip.length));
            }
        }
    }
}
