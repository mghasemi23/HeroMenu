using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

namespace HeroSelection
{
    /// <summary>
    /// Handles Hero Card Functions
    /// </summary>
    public class HeroCardHandler : MonoBehaviour
    {
        public float FadeTime = 0.5f;

        private Image _image;
        private Button _button;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _image = gameObject.GetComponent<Image>();
            _button = gameObject.GetComponent<Button>();
            _canvasGroup = transform.Find("Info Group").GetComponent<CanvasGroup>();
        }

        /// <summary>
        /// Disables Hero Card
        /// </summary>
        public void DisableCard(bool isSelected)
        {
            _button.interactable = false;
            _image.raycastTarget = false;
            if (!isSelected)
                StartCoroutine(FadeOutCard());
        }

        /// <summary>
        /// Enables Hero Card
        /// </summary>
        public void EnableCard(bool isSelected)
        {
            _button.interactable = true;
            _image.raycastTarget = true;
            if (!isSelected)
                StartCoroutine(FadeInCard());

        }

        /// <summary>
        /// Fades out Hero Card image and it's other elements
        /// </summary>
        /// <returns></returns>
        private IEnumerator FadeOutCard()
        {
            float startTime = Time.time;
            while (Time.time < startTime + FadeTime)
            {
                _image.fillAmount = Mathf.Lerp(1f, 0f, (Time.time - startTime) / FadeTime);
                _canvasGroup.alpha = Mathf.Lerp(1f, 0f, (Time.time - startTime) / FadeTime);
                yield return null;
            }
            _image.fillAmount = 0;
            _canvasGroup.alpha = 0;
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Fades in Hero Card image and it's other elements
        /// </summary>
        /// <returns></returns>
        private IEnumerator FadeInCard()
        {
            float startTime = Time.time;
            while (Time.time < startTime + FadeTime)
            {
                _image.fillAmount = Mathf.Lerp(0, 1, (Time.time - startTime) / FadeTime);
                _canvasGroup.alpha = Mathf.Lerp(0, 1, (Time.time - startTime) / FadeTime);
                yield return null;
            }
            _image.fillAmount = 1;
            _canvasGroup.alpha = 1;
        }
    }
}