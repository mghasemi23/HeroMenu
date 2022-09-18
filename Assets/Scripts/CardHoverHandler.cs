using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace HeroSelection
{
    public class CardHoverHandler : MonoBehaviour
    {
        public Vector2 MaxRotation = new Vector2(10, 10);
        public Vector2 MaxShadowReduction = new Vector2(7, 7);
        public bool ShowTooltip = false;
        public Tooltip Tooltip;

        private int _cardLayer;
        private RectTransform _rectTransform;
        private Vector2 _startShadowDistance;
        private Shadow _shadow;
        private string _tooltipText;

        private void Start()
        {
            _cardLayer = LayerMask.NameToLayer("Card");
            _rectTransform = transform as RectTransform;
            _shadow = gameObject.GetComponent<Shadow>();
            _startShadowDistance = _shadow.effectDistance;
        }

        private void Update()
        {
            if (IsPointerOverUIElement())
            {
                var mp = (Vector2)Input.mousePosition;
                var diff = mp - (Vector2)_rectTransform.position;
                var sizeFromCenter = _rectTransform.sizeDelta / 2;
                var x = diff.x / sizeFromCenter.x;
                var y = diff.y / sizeFromCenter.y;

                _rectTransform.rotation = Quaternion.Euler(new Vector2(y * MaxRotation.x, -x * MaxRotation.y));
                _shadow.effectDistance = _startShadowDistance - new Vector2(x * MaxShadowReduction.x, y * MaxShadowReduction.y);


                if (ShowTooltip && Tooltip != null)
                {
                    Tooltip.ShowTooltip(_tooltipText, transform.name);
                }
            }
            else
            {
                _rectTransform.rotation = Quaternion.Euler(Vector3.zero);
                _shadow.effectDistance = _startShadowDistance;

                if (ShowTooltip && Tooltip != null)
                    Tooltip.HideTooltip(transform.name);
            }
        }

        private bool IsPointerOverUIElement()
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResults());
        }

        private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
        {
            for (int index = 0; index < eventSystemRaysastResults.Count; index++)
            {
                var curRaysastResult = eventSystemRaysastResults[index];
                if (curRaysastResult.gameObject == gameObject && curRaysastResult.gameObject.layer == _cardLayer)
                    return true;
            }
            return false;
        }

        private static List<RaycastResult> GetEventSystemRaycastResults()
        {
            var eventData = new PointerEventData(EventSystem.current);
            var raysastResults = new List<RaycastResult>();
            eventData.position = Input.mousePosition;
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }

        public void SetTooltip(string tooltip)
        {
            _tooltipText = tooltip;
        }
    }
}
