using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Game.UI
{
    public class RectTransformTilter : MonoBehaviour
    {
        [Min(0.1f)]
        [SerializeField] private float tiltingSpeed = 10.0f;
        [Min(0.1f)]
        [SerializeField] private float tiltingDuration = 2.0f;
        private RectTransform rectTransform;

        private IEnumerator StartTilting()
        {
            rectTransform.rotation = Quaternion.identity;
            while(true)
            {
                var tween = rectTransform.DOShakeRotation(tiltingDuration, tiltingSpeed, 10, 30, true, ShakeRandomnessMode.Harmonic);
                yield return tween.WaitForCompletion();
            }
        }

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            StartCoroutine(StartTilting());
        }
    }
}
