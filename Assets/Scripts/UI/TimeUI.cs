using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class TimeUI : MonoBehaviour
    {
        private TMP_Text timerUI;

        public void SetTimeText(int minutes, int seconds)
        {
            timerUI.text = minutes.ToString() + ":" + seconds.ToString();
        }
        private void Awake()
        {
            timerUI = GetComponent<TMP_Text>();
        }
    }
}
