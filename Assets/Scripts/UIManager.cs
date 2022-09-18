using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace HeroSelection
{
    /// <summary>
    /// Control and Handle User Interaction with UI elements
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Header("Hero Selectoin")]
        public List<HeroCardHandler> HeroCardList = new List<HeroCardHandler>();
        public float CardMoveTime = 0.5f;
        public Image FullBackground;
        public CanvasGroup Title;

        [Header("Hero Info")]
        public List<HeroInfo> HeroInfos = new List<HeroInfo>();
        public float InfoShowTime = 0.5f;
        public GameObject HeroPanel;
        public CanvasGroup HeroTitle;
        public CanvasGroup Attributes;
        public CanvasGroup Description;
        public CanvasGroup Abilities;
        public CanvasGroup BackButton;
        public Text HeroName;
        public Text HeroDescription;
        public Text[] AttributesTexts = new Text[3];
        public Image[] AbilitiesImages = new Image[3];
        public Text[] AbilitesNames = new Text[3];
        private CardHoverHandler[] AbilitesTooltip = new CardHoverHandler[3];

        [Header("Ability")]
        public Animator Animator;

        private Vector2 _showPos;
        private Vector2 _selectedPos;
        private RectTransform _selectedCard;
        private int _selectedIndex;


        public void Awake()
        {
            _showPos = (HeroCardList[0].gameObject.transform as RectTransform).position;
            for (int i = 0; i < AbilitiesImages.Length; i++)
            {
                AbilitesTooltip[i] = AbilitiesImages[i].gameObject.GetComponent<CardHoverHandler>();
            }
        }

        /// <summary>
        /// Selects a hero and show it's info
        /// </summary>
        /// <param name="selection"></param>
        public void SelectHero(int selection)
        {
            for (int i = 0; i < HeroCardList.Count; i++)
            {
                HeroCardList[i].DisableCard(i == selection);
            }

            _selectedCard = HeroCardList[selection].gameObject.transform as RectTransform;
            _selectedPos = _selectedCard.position;
            _selectedIndex = selection;
            Animator.enabled = true;
            StartCoroutine(ShowHero());
        }

        /// <summary>
        /// Moves hero card into hero showing place
        /// </summary>
        /// <returns></returns>
        private IEnumerator ShowHero()
        {
            float startTime = Time.time;
            while (Time.time < startTime + CardMoveTime)
            {
                _selectedCard.position = Vector3.Lerp(_selectedPos, _showPos, (Time.time - startTime) / CardMoveTime);
                Title.alpha = Mathf.Lerp(1, 0, (Time.time - startTime) / CardMoveTime);
                yield return null;
            }
            _selectedCard.position = _showPos;
            Title.alpha = 0;
            Title.gameObject.SetActive(false);

            LoadAndShowInfo();
        }


        /// <summary>
        /// Loads hero info from property file and shows info on UI
        /// </summary>
        private void LoadAndShowInfo()
        {
            HeroPanel.SetActive(true);
            var info = HeroInfos[_selectedIndex];
            HeroName.text = info.Name;
            HeroDescription.text = info.Description;
            for (int i = 0; i < info.Attributes.Length; i++)
            {
                AttributesTexts[i].text = info.Attributes[i].ToString();
                AbilitiesImages[i].sprite = info.HeroAbiliies[i].Icon;
                AbilitesNames[i].text = info.HeroAbiliies[i].Name;
                AbilitesTooltip[i].SetTooltip(info.HeroAbiliies[i].Description);
            }

            StartCoroutine(ShowHeroInfo());
        }


        /// <summary>
        /// Fades in hero info
        /// </summary>
        private IEnumerator ShowHeroInfo()
        {
            float startTime = Time.time;
            while (Time.time < startTime + CardMoveTime)
            {
                HeroTitle.alpha = Mathf.Lerp(0, 1, (Time.time - startTime) / CardMoveTime);
                Attributes.alpha = Mathf.Lerp(0, 1, (Time.time - startTime) / CardMoveTime);
                Description.alpha = Mathf.Lerp(0, 1, (Time.time - startTime) / CardMoveTime);
                Abilities.alpha = Mathf.Lerp(0, 1, (Time.time - startTime) / CardMoveTime);
                BackButton.alpha = Mathf.Lerp(0, 1, (Time.time - startTime) / CardMoveTime);
                FullBackground.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), (Time.time - startTime) / CardMoveTime);
                yield return null;
            }
            HeroTitle.alpha = 1;
            Attributes.alpha = 1;
            Description.alpha = 1;
            Abilities.alpha = 1;
        }

        /// <summary>
        /// Fades out hero info
        /// </summary>
        private IEnumerator HideHeroInfo()
        {
            float startTime = Time.time;
            while (Time.time < startTime + CardMoveTime)
            {
                _selectedCard.position = Vector3.Lerp(_showPos, _selectedPos, (Time.time - startTime) / CardMoveTime);
                HeroTitle.alpha = Mathf.Lerp(1, 0, (Time.time - startTime) / CardMoveTime);
                Attributes.alpha = Mathf.Lerp(1, 0, (Time.time - startTime) / CardMoveTime);
                Description.alpha = Mathf.Lerp(1, 0, (Time.time - startTime) / CardMoveTime);
                Abilities.alpha = Mathf.Lerp(1, 0, (Time.time - startTime) / CardMoveTime);
                BackButton.alpha = Mathf.Lerp(1, 0, (Time.time - startTime) / CardMoveTime);
                FullBackground.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, (Time.time - startTime) / CardMoveTime);
                yield return null;
            }

            HeroPanel.SetActive(false);
            Title.gameObject.SetActive(true);
            Animator.enabled = false;
            StartCoroutine(ShowSelectionTitle());

            for (int i = 0; i < HeroCardList.Count; i++)
            {
                HeroCardList[i].gameObject.SetActive(true);
                HeroCardList[i].EnableCard(i == _selectedIndex);
            }
        }

        /// <summary>
        /// Fades in selection menu title
        /// </summary>
        private IEnumerator ShowSelectionTitle()
        {
            float startTime = Time.time;
            while (Time.time < startTime + CardMoveTime)
            {
                Title.alpha = Mathf.Lerp(0, 1, (Time.time - startTime) / CardMoveTime);
                yield return null;
            }
            Title.alpha = 1;
        }

        /// <summary>
        /// Plays hero's first ability animation
        /// </summary>
        public void Ability1()
        {
            Animator.SetBool("ability1", true);
        }

        /// <summary>
        /// Plays hero's second ability animation
        /// </summary>
        public void Ability2()
        {
            Animator.SetBool("ability2", true);
        }

        /// <summary>
        /// Plays hero's third ability animation
        /// </summary>
        public void Ability3()
        {
            Animator.SetBool("ability3", true);
        }

        /// <summary>
        /// Moves back to hero selection menu
        /// </summary>
        public void Back()
        {
            StartCoroutine(HideHeroInfo());
        }
    }
}
