using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Objects
{
    public class Timer : MonoBehaviour
    {
        public event Action<float> OnTimerTick;
        [SerializeField] private Text text;
        [SerializeField] private float timeOfTick = 1f;
        private float currentTime = 0f;

        public void StartTimer()
        {
            StartCoroutine(Counter());
        }

        public void ResetTimer()
        {
            currentTime = 0f;
        }

        private IEnumerator Counter()
        {
            while (true)
            {
                currentTime += timeOfTick;
                OnTimerTick?.Invoke(currentTime);
                text.text = currentTime.ToString();
                yield return new WaitForSeconds(timeOfTick);
            }
        }
    }
}