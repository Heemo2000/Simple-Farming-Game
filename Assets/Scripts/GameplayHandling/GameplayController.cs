using Game.Core;
using Game.StateMachineHandling;
using Game.CharacterHandling;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game.GameplayHandling
{
    public class GameplayController : MonoBehaviour
    {
        [Header("UI stuff:")]
        [SerializeField] private Button plantingBtn;
        [SerializeField] private Button wateringBtn;
        [SerializeField] private Button harvestBtn;
        [SerializeField] private Button buyBtn;

        [Header("Time settings: ")]
        [Min(0.1f)]
        [SerializeField] private float wateringTime = 3.0f;
        [Min(0.1f)]
        [SerializeField] private float harvestTime = 3.0f;

        [Header("Other stuff: ")]
        [SerializeField] private GameInput gameInput;
        [SerializeField] private Human playerHuman;
        [SerializeField] private CropManager cropManager;
        [SerializeField] private CropSelector plantingCropSelector;
        [SerializeField] private CropSelector buyingCropSelector;

        private StateMachine stateMachine;

        private NormalState playerState;
        private PlantingState plantingState;
        private WateringState wateringState;
        private HarvestState harvestState;
        private BuyState buyState;

        private GameplayStates currentState = GameplayStates.None;


        private void ChangeToNormalState()
        {
            currentState = GameplayStates.Normal;
        }

        private void ChangeToPlantingState()
        {
            Debug.Log("Changing state to planting.");
            currentState = GameplayStates.Planting;
        }

        private void Awake()
        {
            currentState = GameplayStates.Normal;
            stateMachine = new StateMachine();
            playerState = new NormalState(playerHuman, gameInput);
            plantingState = new PlantingState(playerHuman, cropManager, plantingCropSelector, gameInput);
            wateringState = new WateringState(playerHuman, gameInput);
            harvestState = new HarvestState(playerHuman, gameInput);
            buyState = new BuyState(buyingCropSelector);

            stateMachine.AddTransition(playerState, plantingState, new FuncPredicate(() => currentState == GameplayStates.Planting));
            stateMachine.AddAnyTransition(wateringState, new TimePredicate(wateringTime, () => currentState == GameplayStates.Watering, ChangeToNormalState));
            stateMachine.AddAnyTransition(harvestState, new TimePredicate(harvestTime, () => currentState == GameplayStates.Harvest, ChangeToNormalState));
            stateMachine.AddAnyTransition(buyState, new FuncPredicate(()=> currentState == GameplayStates.Buy));
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            DOTween.Init();
            stateMachine.SetState(playerState);
            plantingBtn.onClick.AddListener(ChangeToPlantingState);
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
            plantingBtn.onClick.RemoveListener(ChangeToPlantingState);
        }
    }
}
