using Game.StateMachineHandling;
using Game.UI;
using UnityEngine;

namespace Game.GameplayHandling
{
    public class GameOverState : IState
    {
        private BuyManager buyManager;
        private UIManager uiManager;
        private Page timePanel;
        private Page overallMoneyPanel;
        private Page gameOverPanel;
        private GameOverUI gameOverUI;
        private PlayerController playerController;
        
        public GameOverState(GameplayController controller)
        {
            this.buyManager = controller.BuyManager;
            this.uiManager = controller.UIManager;
            this.timePanel = controller.TimePanel;
            this.overallMoneyPanel = controller.OverallMoneyPanel;
            this.gameOverPanel = controller.GameOverPanel;
            this.gameOverUI = controller.GameOverUI;
            this.playerController = controller.PlayerHuman;
        }

        public void OnEnter()
        {
            Debug.Log("Now, in game over state");
            this.timePanel.exitOnNewPagePush = true;
            this.overallMoneyPanel.exitOnNewPagePush = true;
            uiManager.PopAllPages();
            uiManager.PushPage(this.gameOverPanel);
            this.gameOverUI.SetTotalMoney(this.buyManager.CurrentMoney);
        }

        public void OnUpdate()
        {
            this.playerController.HandleMovement(Vector2.zero);
        }

        public void OnFixedUpdate()
        {
            
        }

        public void OnLateUpdate()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}
