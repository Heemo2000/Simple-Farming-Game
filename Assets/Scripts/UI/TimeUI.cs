using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class TimeUI : MonoBehaviour
    {
        private TMP_Text timerUI;

        public void SetTimeText(int minutes, int seconds)
        {
            string secondsString = seconds.ToString();
            if(seconds >= 0 && seconds <= 9)
            {
                secondsString = "0" + secondsString;
            }
            timerUI.text = minutes.ToString() + ":" + secondsString;
        }
        private void Awake()
        {
            timerUI = GetComponent<TMP_Text>();
        }
    }
}
