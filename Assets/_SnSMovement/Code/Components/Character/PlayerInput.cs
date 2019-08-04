﻿using UnityEngine;
using UnityEngine.Events;

namespace SnSMovement.Character
{
	[System.Serializable]
	public class InputAxisEvent : UnityEvent<float>{}

	[System.Serializable]
	public class InputButtonEvent : UnityEvent { }

	public class PlayerInput : MonoBehaviour
	{
		public string horizontalAxis = "Horizontal";
		public string verticalAxis = "Vertical";
		public float jumpSens = 0.1f;

		public InputAxisEvent onHorizontalInputAxis = new InputAxisEvent ();
		public InputButtonEvent onJump = new InputButtonEvent();
		public UnityEvent onFlare = new UnityEvent();

		public bool isPaused = false;

		private void Update ()
		{
			if (!isPaused)
			{
				onHorizontalInputAxis.Invoke (Input.GetAxisRaw(horizontalAxis));
				if (Input.GetAxisRaw(verticalAxis) > jumpSens)
				{
					onJump.Invoke();
				}

				if (Input.GetButtonDown("Fire1"))
				{
					onFlare.Invoke();	
				}
			}
			if (Input.GetButtonDown("Cancel"))
			{
				InGameUIManager.Instance.PauseGame(!isPaused);
				isPaused = !isPaused;
			}
		}
	}
}