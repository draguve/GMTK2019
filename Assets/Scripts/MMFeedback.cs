using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MoreMountains.Tools
{
    /// <summary>
    /// This class is to be used from other classes, to act as a center point for various feedbacks. 
    /// It's meant to help setup and trigger feedbacks such as vfx, sounds, camera zoom or shake, etc, from an automated entry points in other classes inspectors.
    /// </summary>
	[Serializable]
	public class MMFeedback
    {
        [Header("Animation")]
        public bool UpdateAnimator;
        [Condition("UpdateAnimator", true)]
        public Animator FeedbackAnimator;
        [Condition("UpdateAnimator", true)]
        public string AnimatorTriggerParameterName;
        [Condition("UpdateAnimator", true)]
        public string AnimatorBoolParameterName;
        
        [Header("Particles")]
        /// a particle system already present in your object that will play when the feedback is played, and stopped when it's stopped
        public ParticleSystem Particles;

        [Header("Instantiated VFX")]
        /// whether or not a VFX (or other...) object should be instantiated once when the feedback is played
        public bool InstantiateVFX;
        [Condition("InstantiateVFX", true)]
        /// the vfx object to instantiate
        public GameObject VfxToInstantiate;
        [Condition("InstantiateVFX", true)]
        /// the position offset at which to instantiate the vfx object
        public Vector3 VfxPositionOffset;
        [Condition("InstantiateVFX", true)]
        /// whether or not we should create automatically an object pool for this vfx
        public bool VfxCreateObjectPool;
        [Condition("InstantiateVFX", true)]
        /// the initial and planned size of this object pool

        [Header("Flicker")]

        public bool FlickerRenderer;
        /// whether or not the renderer should flicker when playing this feedback
        [Condition("FlickerRenderer", true)]
        public Renderer FlickeringRenderer;
        /// the duration of the flicker when getting damage
        
        [Header("Sounds")]
        /// a sound fx to play when this feedback is played
        public AudioClip Sfx;
        
        protected GameObject _newGameObject;
        protected Color _initialFlickerColor;

        /// <summary>
        /// This method needs to be called by the parent class to initialize the various feedbacks
        /// </summary>
        public virtual void Initialization(GameObject gameObject = null)
        {

            if (InstantiateVFX && VfxCreateObjectPool)
            {
                GameObject objectPoolGo = new GameObject();
                objectPoolGo.name = "FeedbackObjectPool";
            }
            if (FlickerRenderer && (FlickeringRenderer != null))
            {
                if(FlickeringRenderer.material.HasProperty("_Color"))
                {
                    _initialFlickerColor = FlickeringRenderer.material.color;
                }
            }
            if (FlickerRenderer && (FlickeringRenderer == null) && (gameObject != null))
            {
                var renderer = gameObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    FlickeringRenderer = renderer;
                }
                if (FlickeringRenderer == null)
                {
                    FlickeringRenderer = gameObject.GetComponentInChildren<Renderer>();
                }
                if (FlickeringRenderer != null)
                {
                    if (FlickeringRenderer.material.HasProperty("_Color"))
                    {
                        _initialFlickerColor = FlickeringRenderer.material.color;
                    }
                }
            }            
        }

        public virtual void ResetFlickerColor()
        {
            if (FlickerRenderer && (FlickeringRenderer != null))
            {
                if (FlickeringRenderer.material.HasProperty("_Color"))
                {
                    FlickeringRenderer.material.color = _initialFlickerColor;
                }
            }
        }

        /// <summary>
        /// Plays all the feedbacks that were enabled for this
        /// </summary>
        /// <param name="position"></param>
        public virtual void Play(Vector3 position, MonoBehaviour monobehaviour = null)
        {
            if (UpdateAnimator)
            {
                if (AnimatorTriggerParameterName != "")
                {
                    FeedbackAnimator.SetTrigger(AnimatorTriggerParameterName);
                }

                if (AnimatorBoolParameterName != "")
                {
                    FeedbackAnimator.SetBool(AnimatorBoolParameterName, true);
                }
            }

            // instantiated particles
            if (InstantiateVFX && VfxToInstantiate != null)
            {
                _newGameObject = GameObject.Instantiate(VfxToInstantiate) as GameObject;
                    _newGameObject.transform.position = position + VfxPositionOffset;
            }

            // Particles
            if (Particles != null)
            {
                Particles.Play();
            }
            
            // Sounds
            if (Sfx != null)
            {
                MMSfxEvent.Trigger(Sfx);
            }
        }

        /// <summary>
        /// Stops all the feedbacks that need stopping
        /// </summary>
        public virtual void Stop()
		{
            // Particles
            if (Particles != null)
			{
				Particles.Stop();
			}

            if (UpdateAnimator)
            {
                if (AnimatorBoolParameterName != "")
                {
                    FeedbackAnimator.SetBool(AnimatorBoolParameterName, false);
                }
            }
        }
	}
}
