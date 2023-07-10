using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.limphus.utilities
{
    public class AnimationHandler : MonoBehaviour
    {
        [Header("Attributes - Animation")]
        [SerializeField] protected Animator animator;

        protected string currentState;

        protected void PlayAnimation(string newState)
        {
            //stops the same animation from interrupting itself.
            if (currentState == newState) return;

            //play the animation
            animator.Play(newState);

            //reassign the current state
            currentState = newState;
        }

        protected void SetParamater(string paramater, int value)
        {
            animator.SetInteger(paramater, value);
        }

        protected void SetParamater(string paramater, float value)
        {
            animator.SetFloat(paramater, value);
        }

        protected void SetParamater(string paramater, bool value)
        {
            animator.SetBool(paramater, value);
        }

        protected void SetTrigger(string paramater, bool value)
        {
            if (value) animator.SetTrigger(paramater);
            else animator.ResetTrigger(paramater);
        }

    }
}