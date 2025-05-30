using UnityEngine;
using Game.StateMachineHandling;
using Game.Core;
using Game.CharacterHandling;


namespace Game.GameplayHandling
{
    public class GameplayController : MonoBehaviour
    {
        [SerializeField] private GameInput gameInput;
        [SerializeField] private Human playerHuman;

        private StateMachine stateMachine;
        private PlantingState plantingState;
        private PlayerState playerState;
        private bool isPlanting = false;


        private void EnterPlantingMode()
        {
            isPlanting = true;
        }

        private void ExitPlantingMode()
        {
            isPlanting = false;
        }

        private void Awake()
        {
            
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            gameInput.OnEnterPlantMode += EnterPlantingMode;
            gameInput.OnExitPlantMode += ExitPlantingMode;

            stateMachine = new StateMachine();
            plantingState = new PlantingState(gameInput);
            playerState = new PlayerState(playerHuman, gameInput);
            stateMachine.AddTransition(playerState, plantingState, new FuncPredicate(() => isPlanting == true));
            stateMachine.AddTransition(plantingState, playerState, new FuncPredicate(() => isPlanting == false));
            stateMachine.SetState(playerState);
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine.OnUpdate();
        }

        private void FixedUpdate()
        {
            stateMachine.OnFixedUpdate();
        }

        private void LateUpdate()
        {
            stateMachine.OnLateUpdate();
        }

        private void OnDestroy()
        {
            gameInput.OnEnterPlantMode -= EnterPlantingMode;
            gameInput.OnExitPlantMode -= ExitPlantingMode;
        }
    }
}
