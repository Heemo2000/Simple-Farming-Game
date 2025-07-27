using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Game.UI
{
    public class RectTransformTilter : MonoBehaviour
    {
        [Min(0.1f)]
        [SerializeField] private float tiltingDuration = 2.0f;
        private RectTransform rectTransform;

        private IEnumerator StartTilting()
        {
            rectTransform.rotation = Quaternion.identity;
            while(true)
            {
                var tween = rectTransform.DOPunchRotation(Vector3.forward * 180.0f, tiltingDuration, 5, 5.0f);
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
