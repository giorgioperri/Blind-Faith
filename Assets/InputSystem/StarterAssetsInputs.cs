using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool pause;
		public bool sprint;
		//added crouch state if we want to implement it
		public bool crouch;
		//added raiseLantern state
		public bool hasRaisedLantern;
		public bool isInteracting;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}
		
		public void OnPause(InputValue value)
		{
			PauseInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}
		public void OnRaiseLantern(InputValue value)
		{
			RaiseLanternInput(value.isPressed);
		}
		public void OnInteract(InputValue value)
		{
			InteractInput(value.isPressed);	
		}

#endif
		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void InteractInput(bool isInteracting)
		{
			this.isInteracting = isInteracting;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}
		
		public void PauseInput(bool newPauseState)
		{
			pause = newPauseState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void RaiseLanternInput(bool newRaiseLanternState)
		{
			hasRaisedLantern = newRaiseLanternState;
		}

		private void Update()
		{
			if (Mouse.current.leftButton.wasPressedThisFrame)
			{
				SetCursorState(!GameManager.Instance.isPaused);
			}
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}


	}
	
}
