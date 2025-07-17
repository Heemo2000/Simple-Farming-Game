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
            var cropTypesValues = Enum.GetValues(typeof(CropType));
            for (int i = 0; i < cropTypesValues.Length; i++)
            {
                buttons[i].onClick.AddListener(() => SelectCropType((CropType)cropTypesValues.GetValue(i)));
            }
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
