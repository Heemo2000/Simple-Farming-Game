using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;
using Game.UI;
using Game.Core;
using Game.StateMachineHandling;
using Game.CharacterHandling;
using Game.CameraHandling;
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
        [SerializeField] private Button goBackFromPlantingBtn;
        [SerializeField] private Button goBackFromBuyBtn;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private Page mainPanel;
        [SerializeField] private Page plantingPanel;
        [SerializeField] private Page wateringPanel;
        [SerializeField] private Page harvestingPanel;
        [SerializeField] private Page buyPanel;

        [Header("Time settings: ")]
        [Min(0.1f)]
        [SerializeField] private float wateringTime = 3.0f;
        [Min(0.1f)]
        [SerializeField] private float harvestTime = 3.0f;

        [Header("Watering Settings: ")]
        [Min(0.1f)]
        [SerializeField] private float wateringRadius = 4.0f;
        [Min(0.1f)]
        [SerializeField] private float wateringInterval = 0.1f;

        [Header("Harvesting Settings: ")]
        [Min(0.1f)]
        [SerializeField] private float harvestingRadius = 4.0f;
        [Min(0.1f)]
        [SerializeField] private float harvestingInterval = 0.1f;

        [Header("Other stuff: ")]
        [SerializeField] private GameInput gameInput;
        [SerializeField] private Human playerHuman;
        [SerializeField] private CropManager cropManager;
        [SerializeField] private CropSelector plantingCropSelector;
        [SerializeField] private CropSelector buyingCropSelector;
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private CinemachineCamera playerCamera;
        [SerializeField] private CinemachineCamera topViewCamera;
        [SerializeField] private LayerMask cropLayerMask;

        private StateMachine stateMachine;

        private NormalState playerState;
        private PlantingState plantingState;
        private WateringState wateringState;
        private HarvestState harvestState;
        private BuyState buyState;

        private GameplayStates currentState = GameplayStates.None;

        public GameInput GameInput { get => gameInput; }
        public Human PlayerHuman { get => playerHuman; }
        public CropManager CropManager { get => cropManager; }
        public CropSelector PlantingCropSelector { get => plantingCropSelector; }
        public CropSelector BuyingCropSelector { get => buyingCropSelector; }
        public CameraManager CameraManager { get => cameraManager; }
        public CinemachineCamera PlayerCamera { get => playerCamera; }
        public CinemachineCamera TopViewCamera { get => topViewCamera; }
        public float WateringRadius { get => wateringRadius;}
        public float WateringInterval { get => wateringInterval;}
        public float HarvestingRadius { get => harvestingRadius;}
        public float HarvestingInterval { get => harvestingInterval;}
        public LayerMask CropLayerMask { get => cropLayerMask;}
        public UIManager UIManager { get => uiManager; }
        public Page WateringPanel { get => wateringPanel;}
        public Page HarvestingPanel { get => harvestingPanel;}

        private void ChangeToNormalState()
        {
            currentState = GameplayStates.Normal;
        }

        private void ChangeToPlantingState()
        {
            Debug.Log("Changing state to planting.");
            currentState = GameplayStates.Planting;
        }

        private void ChangeToWateringState()
        {
            Debug.Log("Changing state to watering.");
            currentState = GameplayStates.Watering;
        }

        private void ChangeToHarvestingState()
        {
            Debug.Log("Changing state to harvesting.");
            currentState = GameplayStates.Harvest;
        }

        private void ChangeToBuyState()
        {
            Debug.Log("Changing state to buying.");
            currentState = GameplayStates.Buy;
        }
        private void Awake()
        {
            currentState = GameplayStates.Normal;
            stateMachine = new StateMachine();
            playerState = new NormalState(playerHuman, cameraManager, playerCamera, uiManager, mainPanel, gameInput);
            plantingState = new PlantingState(cropManager, plantingCropSelector, cameraManager, topViewCamera, uiManager, mainPanel, plantingPanel, gameInput);
            wateringState = new WateringState(this);
            harvestState = new HarvestState(this);
            buyState = new BuyState(buyingCropSelector, uiManager, mainPanel, buyPanel);

            stateMachine.AddTransition(playerState, plantingState, new FuncPredicate(() => currentState == GameplayStates.Planting));
            stateMachine.AddAnyTransition(playerState, new FuncPredicate(() => currentState == GameplayStates.Normal));
            stateMachine.AddAnyTransition(wateringState, new TimePredicate(wateringTime, () => currentState == GameplayStates.Watering, ChangeToNormalState));
            stateMachine.AddAnyTransition(harvestState, new TimePredicate(harvestTime, () => currentState == GameplayStates.Harvest, ChangeToNormalState));
            stateMachine.AddAnyTransition(buyState, new FuncPredicate(()=> currentState == GameplayStates.Buy));
        }


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            plantingBtn.onClick.AddListener(ChangeToPlantingState);
            wateringBtn.onClick.AddListener(ChangeToWateringState);
            harvestBtn.onClick.AddListener(ChangeToHarvestingState);
            buyBtn.onClick.AddListener(ChangeToBuyState);

            goBackFromPlantingBtn.onClick.AddListener(ChangeToNormalState);
            goBackFromBuyBtn.onClick.AddListener(ChangeToNormalState);

            DOTween.Init();
            stateMachine.SetState(playerState);
            plantingBtn.onClick.AddListener(ChangeToPlantingState);
            Application.targetFrameRate = 60;
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

        private void OnDrawGizmos()
        {
            if(playerHuman == null)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(playerHuman.transform.position, wateringRadius);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(playerHuman.transform.position, harvestingRadius);
        }

        private void OnDestroy()
        {
            plantingBtn.onClick.RemoveListener(ChangeToPlantingState);
        }
    }
}
