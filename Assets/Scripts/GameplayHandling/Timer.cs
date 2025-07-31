using System;
using System.Collections;
using UnityEngine;

namespace Game.GameplayHandling
{
    public class Timer
    {
        public Action<int, int> OnTimeModified;
        public Action OnTimeUp;

        private TimeSpan remainingTime;
        private TimeSpan amountToSubtract;

        public TimeSpan RemainingTime { get => remainingTime; }

        public Timer(int minutes, int seconds)
        {
            remainingTime = new TimeSpan(0, minutes, seconds);
            amountToSubtract = new TimeSpan(0, 0, 1);
        }

        public void StartTimer(MonoBehaviour mb)
        {
            mb.StartCoroutine(StartTimerCoroutine());
        }

        private IEnumerator StartTimerCoroutine()
        {
            while(remainingTime.TotalSeconds > 0)
            {
                remainingTime = remainingTime.Subtract(amountToSubtract);
                OnTimeModified?.Invoke(remainingTime.Minutes, remainingTime.Seconds);
                yield return new WaitForSeconds(1.0f);
            }

            OnTimeModified?.Invoke(remainingTime.Minutes, remainingTime.Seconds);
            OnTimeUp?.Invoke();
        }
    }
}
