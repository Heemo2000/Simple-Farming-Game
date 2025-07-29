using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game.UI
{
    public class HoverButton : MonoBehaviour, IPointerMoveHandler, IPointerExitHandler, IPointerClickHandler
    {
        private RectTransform rectTransform;
        public UnityEvent OnHover;
        public UnityEvent OnHoverExit;
        public UnityEvent OnClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }
        public void OnPointerMove(PointerEventData eventData)
        {
            OnHover?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnHoverExit?.Invoke();
        }

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }
}
