using Game.GameplayHandling;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GameplayHandling
{
    [System.Serializable]
    public class CropSelectorData
    {
        public CropType cropType;
        public Button selectButton;
    }

    public class CropSelector : MonoBehaviour
    {
        [SerializeField] private CropSelectorData[] selectData;

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

            foreach (var data in selectData)
            {
                CropType type = data.cropType;
                data.selectButton.onClick.AddListener(() => SelectCropType(type));
            }
        }

        private void OnDestroy()
        {
            foreach (var data in selectData)
            {
                data.selectButton.onClick.RemoveAllListeners();
            }
        }
    }
}
