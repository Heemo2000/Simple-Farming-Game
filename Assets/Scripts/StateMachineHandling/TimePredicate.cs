using System;
using UnityEngine;

namespace Game.StateMachineHandling
{
    public class TimePredicate : IPredicate, IResetable
    {
        private readonly float duration;
        private readonly Func<bool> startCondition;
        private readonly Action onTimerEnd;

        private float timer = 0f;
        private bool isTimerRunning = false;

        public TimePredicate(float duration, Func<bool> startCondition, Action onTimerEnd = null)
        {
            this.duration = duration;
            this.startCondition = startCondition;
            this.onTimerEnd = onTimerEnd;
        }

        public bool Evaluate()
        {
            if (!isTimerRunning && startCondition.Invoke())
            {
                isTimerRunning = true;
                timer = duration;
            }

            if (isTimerRunning)
            {
                timer -= Time.deltaTime;
                if (timer <= 0f)
                {
                    isTimerRunning = false;
                    onTimerEnd?.Invoke();
                    return false;
                }

                return true;
            }

            return false;
        }

        public void Reset()
        {
            timer = 0f;
            isTimerRunning = false;
        }
    }
}
