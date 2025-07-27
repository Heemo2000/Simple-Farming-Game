using UnityEngine;
using TMPro;

namespace Game.UI
{
    public class PlantingCropUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text countUI;

        public void SetCount(int count)
        {
            countUI.text = count.ToString();
        }
    }
}
