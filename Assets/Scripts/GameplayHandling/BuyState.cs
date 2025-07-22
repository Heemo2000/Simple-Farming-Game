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

        public BuyState(CropSelector cropSelector, UIManager uiManager, Page mainPanel, Page buyPanel)
        {
            this.cropSelector = cropSelector;
            this.uiManager = uiManager;
            this.mainPanel = mainPanel;
            this.buyPanel = buyPanel;
        }

        public void OnEnter()
        {
            this.mainPanel.exitOnNewPagePush = true;
            this.uiManager.PushPage(buyPanel);
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
