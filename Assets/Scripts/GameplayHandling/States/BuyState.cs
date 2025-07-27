using UnityEngine;
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
        private BuyManager buyManager;

        public BuyState(GameplayController controller)
        {
            this.cropSelector = controller.BuyingCropSelector;
            this.uiManager = controller.UIManager;
            this.mainPanel = controller.MainPanel;
            this.buyPanel = controller.BuyPanel;
            this.buyManager = controller.BuyManager;
        }

        public void OnEnter()
        {
            this.mainPanel.exitOnNewPagePush = true;
            this.uiManager.PushPage(buyPanel);
            this.buyManager.LoadData();
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
        }
    }
}
