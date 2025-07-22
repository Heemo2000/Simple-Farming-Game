using Game.GameplayHandling;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GameplayHandling
{
    public class CropSelector : MonoBehaviour
    {
        [SerializeField] private Button[] buttons;

        private CropType selectedCropType = CropType.None;
        public CropType SelectedCropType { get => selectedCropType; }

        private void SelectCropType(CropType cropType)
        {
            selectedCropType = cropType;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            selectedCropType = CropType.Broccoli;
            buttons[0].onClick.AddListener(() => SelectCropType(CropType.Broccoli));
            buttons[1].onClick.AddListener(() => SelectCropType(CropType.Mushroom));
            buttons[2].onClick.AddListener(() => SelectCropType(CropType.Cabbage));
            buttons[3].onClick.AddListener(() => SelectCropType(CropType.Carrot));

        }

        private void OnDestroy()
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].onClick.RemoveAllListeners();
            }
        }
    }
}
