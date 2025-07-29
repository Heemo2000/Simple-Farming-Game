using Game.StateMachineHandling;
using Game.UI;

namespace Game.GameplayHandling
{
    public class BuyState : IState
    {
        private CropSelector cropSelector;
        private UIManager uiManager;
        private Page mainPanel;
        private Page buyPanel;
        private Inventory inventory;
        private BuyManager buyManager;

        public BuyState(GameplayController controller)
        {
            this.cropSelector = controller.BuyingCropSelector;
            this.uiManager = controller.UIManager;
            this.mainPanel = controller.MainPanel;
            this.buyPanel = controller.BuyPanel;
            this.inventory = controller.Inventory;
            this.buyManager = controller.BuyManager;
        }

        public void OnEnter()
        {
            this.mainPanel.exitOnNewPagePush = true;
            this.uiManager.PushPage(buyPanel);
            this.inventory.LoadData();
            this.buyManager.LoadData();
            this.cropSelector.OnCropClicked.AddListener(BuyCrop);
        }

        public void OnUpdate()
        {

        }

        public void OnFixedUpdate()
        {
            
        }

        public void OnLateUpdate()
        {
            
        }

        public void OnExit()
        {
            this.uiManager.PopPage();
            this.cropSelector.OnCropClicked.RemoveListener(BuyCrop);
        }

        private void BuyCrop(CropType cropType)
        {
            this.buyManager.BuyCrop(cropType);
        }
    }
}
