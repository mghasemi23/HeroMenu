using UnityEngine;
using UnityEngine.UI;

namespace HeroSelection
{
    public class Tooltip : MonoBehaviour
    {
        private Camera _uiCamera;
        private Text _tooltipText;
        private RectTransform _backgroundRectTransform;
        private string _starter;

        private void Awake()
        {
            _backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
            _tooltipText = transform.Find("text").GetComponent<Text>();
        }

        private void Update()
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, _uiCamera, out localPoint);
            transform.localPosition = localPoint;
        }

        /// <summary>
        /// Shows the tooltip on mouse position
        /// </summary>
        public void ShowTooltip(string tooltipString, string starter)
        {
            _starter = starter;
            gameObject.SetActive(true);

            _tooltipText.text = tooltipString;
            float textPaddingSize = 4f;
            Vector2 backgroundSize = new Vector2(_tooltipText.preferredWidth + textPaddingSize * 2f, _tooltipText.preferredHeight + textPaddingSize * 2f);
            _backgroundRectTransform.sizeDelta = backgroundSize;
        }

        /// <summary>
        /// Hides the tooltip
        /// </summary>
        public void HideTooltip(string starter)
        {
            if (_starter == starter)
                gameObject.SetActive(false);
        }

    }
}