using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mixin.Utils
{
    public static class AnimationHelper
    {
        public static void Play(this Animator animator, AnimationClip clip)
        {
            // Generate a unique name for the animation state or trigger
            int animationHash = Animator.StringToHash(clip.name);

            // Set the trigger parameter of the Animator to true to play the animation
            animator.Play(animationHash);
        }
    }
}