using UnityEngine;

namespace HeroSelection
{

    public class AnimationEvent : MonoBehaviour
    {
        private Animator animator;


        public void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void DisableAbility1()
        {
            animator.SetBool("ability1", false);
        }

        public void DisableAbility2()
        {
            animator.SetBool("ability2", false);
        }

        public void DisableAbility3()
        {
            animator.SetBool("ability3", false);
        }
    }
}
