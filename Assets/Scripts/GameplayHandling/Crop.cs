using System.Collections;
using UnityEngine;
using DG.Tweening;
using Game.Core;
using Game.ObjectPoolHandling;

namespace Game.GameplayHandling
{
    public class Crop : MonoBehaviour
    {
        [SerializeField]
        private CropType type = CropType.None;

        [SerializeField]
        private CropGrowthStageData[] stages;
        
        [SerializeField]
        [Min(0.1f)]
        private float punchAnimScale = 2.0f;

        [SerializeField]
        [Min(0.1f)]
        private float punchAnimDuration = 0.3f;

        private ObjectPool<Crop> cropPool = null;
        private int currentStageIndex = -1;
        private Coroutine growCoroutine = null;
        private bool isProcessing = false;
        private CropState state = CropState.None;

        public ObjectPool<Crop> CropPool { get => cropPool; set => cropPool = value; }
        public CropType Type { get => type; }
        public void Grow()
        {
            if(growCoroutine == null && isProcessing == false)
            {
                growCoroutine = StartCoroutine(GrowForSeconds());
            }
        }

        public void Harvest()
        {
            if(state == CropState.Harvestable)
            {
                if(cropPool != null && ServiceLocator.ForSceneOf(this).TryGetService<CropManager>(out CropManager manager))
                {
                    manager.DespawnCrop(this);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }

        private IEnumerator GrowForSeconds()
        {
            isProcessing = true;
            if(currentStageIndex + 1 >=  stages.Length)
            {
                growCoroutine = null;
                isProcessing = false;
                yield break;
            }

            isProcessing = true;
            var lastStage = (currentStageIndex > 0) ? stages[currentStageIndex - 1] : null;
            
            currentStageIndex++;

            var currentStage = stages[currentStageIndex];

            yield return new WaitForSeconds(currentStage.time);
            Tween tween = null;
            if (lastStage != null)
            {
                tween = lastStage.graphics.transform.DOScale(0.01f, 0.2f);
                yield return tween.WaitForCompletion();
                lastStage.graphics.SetActive(false);
            }

            currentStage.graphics.SetActive(true);
            
            var currentStageScale = currentStage.graphics.transform.localScale;
            tween = currentStage.graphics.transform.DOPunchScale(currentStageScale * punchAnimScale, punchAnimDuration);
            
            yield return tween.WaitForCompletion();

            isProcessing = false;

            if (currentStageIndex < stages.Length)
            {
                state = CropState.Growing;
            }
            else
            {
                state = CropState.Harvestable;
            }

            growCoroutine = null;
        }

        void Awake()
        {
            currentStageIndex = -1;
            state = CropState.None;
        }

        private void Start()
        {
            foreach (var stage in stages)
            {
                if (stage != null)
                {
                    stage.graphics.SetActive(false);
                }
            }
        }
    }
}
