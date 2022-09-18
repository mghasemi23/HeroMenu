using System;
using UnityEngine;
using UnityEngine.UI;

namespace HeroSelection
{
    /// <summary>
    /// Class that contain each hero's info
    /// </summary>
    [CreateAssetMenu(menuName = "Heros/Hero Info", order = 00)]
    public class HeroInfo : ScriptableObject
    {
        public string ID;
        public string Name;
        public string Description;
        public HeroAbility[] HeroAbiliies;
        public int[] Attributes;
    }

    [Serializable]
    public class HeroAbility
    {
        public string Name;
        public string Description;
        public Sprite Icon;
    }
}