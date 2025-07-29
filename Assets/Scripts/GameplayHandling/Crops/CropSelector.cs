using UnityEngine;
using UnityEngine.Events;
using Game.UI;

namespace Game.GameplayHandling
{
    [System.Serializable]
    public class CropSelectorData
    {
        public CropType cropType;
        public HoverButton selectButton;
    }

    public class CropSelector : MonoBehaviour
    {
        [SerializeField] private CropSelectorData[] selectData;

        private CropType selectedCropType = CropType.None;
        private bool isInsideButton = false;

        public UnityEvent<CropType> OnCropClicked;
        public CropType SelectedCropType { get => selectedCropType; }
        public bool IsInsideButton { get => isInsideButton;}


        private void SelectCropType(CropType cropType)
        {
            selectedCropType = cropType;
            isInsideButton = true;
        }

        private void MakeIsInsideBoolFalse()
        {
            isInsideButton = false;
        }

        private void ClickOnCropType(CropType cropType)
        {
            OnCropClicked?.Invoke(cropType);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            selectedCropType = CropType.Broccoli;

            foreach (var data in selectData)
            {
                CropType type = data.cropType;
                data.selectButton.OnHover.AddListener(() => SelectCropType(type));
                data.selectButton.OnHoverExit.AddListener(MakeIsInsideBoolFalse);
                data.selectButton.OnClick.AddListener(() => ClickOnCropType(type));
            }
        }

        private void OnDestroy()
        {
            foreach (var data in selectData)
            {
                data.selectButton.OnHover.RemoveAllListeners();
                data.selectButton.OnHoverExit.RemoveAllListeners();
            }
        }
    }
}
