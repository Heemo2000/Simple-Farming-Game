using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.GameplayHandling
{
    public class Timer : MonoBehaviour
    {
        [Range(0,12)]
        [SerializeField] private int minutes = 3;
        [Range(0, 59)]
        [SerializeField] private int seconds = 0;

        public UnityEvent<int, int> OnTimeModified;
        public UnityEvent OnTimeUp;

        private TimeSpan remainingTime;
        private TimeSpan amountToSubtract;

        private IEnumerator StartTimer()
        {
            while(remainingTime.TotalSeconds > 0)
            {
                remainingTime = remainingTime.Subtract(amountToSubtract);
                OnTimeModified?.Invoke(remainingTime.Minutes, remainingTime.Seconds);
                yield return new WaitForSeconds(1.0f);
            }

            OnTimeUp?.Invoke();
        }

        private void Awake()
        {
            remainingTime = new TimeSpan(0, minutes, seconds);
            amountToSubtract = new TimeSpan(0, 0, 1);
        }

        private void Start()
        {
            StartCoroutine(StartTimer());
        }
    }
}
