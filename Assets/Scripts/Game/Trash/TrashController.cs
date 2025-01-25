using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using UnityEngine;

namespace GGJ25.Game.Trash
{
    public class TrashController : MonoBehaviour
    {
        public static List<TrashController> TrashControllers = new List<TrashController>();

        [SerializeField]
        private float frozenSpeedLerp = 30;
        [SerializeField]
        private float rotateSpeed = 5;

        private new Rigidbody2D rigidbody;

        private Vector2 targetVelocity;
        private Vector2 preFreezeVelocity;

        //private float currentRotateSpeed;
        //private float targetRotateSpeed;

        private float timeCreated;
        private float freezeStartTime;

        private void Awake()
        {
            timeCreated = Random.Range(0, 360f);

            rigidbody = GetComponent<Rigidbody2D>();

            //currentRotateSpeed = rotateSpeed;
            //targetRotateSpeed = rotateSpeed;

            TrashControllers.Add(this);
        }

        private void Update()
        {
            //currentRotateSpeed = Mathf.Lerp(currentRotateSpeed, targetRotateSpeed, frozenSpeedLerp * Time.unscaledDeltaTime);

            //transform.eulerAngles = new Vector3(0, 0, timeCreated + Time.time * currentRotateSpeed);

            if (freezeStartTime > 0)
            {
                rigidbody.linearVelocity = Vector2.Lerp(rigidbody.linearVelocity, targetVelocity, frozenSpeedLerp * Time.unscaledDeltaTime);
            }
        }

        private IEnumerator InitiateFreezeCoroutine()
        {
            yield return new WaitForSeconds(5);

            targetVelocity = preFreezeVelocity;
            //targetRotateSpeed = rotateSpeed;
        }

        public void InitiateFreeze()
        {
            if (freezeStartTime > 0)
            {
                return;
            }

            freezeStartTime = Time.time;
            preFreezeVelocity = rigidbody.linearVelocity;
            targetVelocity = preFreezeVelocity * 0.2f;
            //targetRotateSpeed = rotateSpeed * 0.2f;
            StartCoroutine(InitiateFreezeCoroutine());
        }

        private void OnDestroy()
        {
            TrashControllers.Remove(this);
        }
    }
}
