using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GGJ25.Game.Trash
{
    public class TrashController : MonoBehaviour
    {
        public static List<TrashController> TrashControllers = new List<TrashController>();

        [SerializeField]
        private float frozenSpeedLerp = 30;

        private new Rigidbody2D rigidbody;

        private Vector2 targetVelocity;
        private Vector2 preFreezeVelocity;

        private float freezeStartTime;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();

            TrashControllers.Add(this);
        }

        private void Update()
        {
            if(freezeStartTime > 0)
            {
                rigidbody.linearVelocity = Vector2.Lerp(rigidbody.linearVelocity, targetVelocity, frozenSpeedLerp * Time.unscaledDeltaTime);
            }
        }

        private IEnumerator InitiateFreezeCoroutine()
        {
            yield return new WaitForSeconds(5);

            targetVelocity = preFreezeVelocity;
        }

        public void InitiateFreeze()
        {
            if(freezeStartTime > 0)
            {
                return;
            }

            freezeStartTime = Time.time;
            preFreezeVelocity = rigidbody.linearVelocity;
            targetVelocity = preFreezeVelocity * 0.2f;
            StartCoroutine(InitiateFreezeCoroutine());
        }

        private void OnDestroy()
        {
            TrashControllers.Remove(this);
        }
    }
}
