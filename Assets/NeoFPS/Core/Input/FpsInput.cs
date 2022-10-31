using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NeoFPS.Constants;

namespace NeoFPS
{
	public abstract class FpsInput : FpsInputBase
	{
        protected override bool isInputActive
        {
			get { return (NeoFpsInputManager.instance is NeoFpsInputManager); }
		}

        public float GetAxis (FpsInputAxis axis)
		{
			if (!hasFocus || !isInputActive)
				return 0f;
			
			switch (axis)
			{
				case FpsInputAxis.MouseX:
					return Input.GetAxis (NeoFpsInputManager.mouseXAxis);
				case FpsInputAxis.MouseY:
					return Input.GetAxis (NeoFpsInputManager.mouseYAxis);
				case FpsInputAxis.MouseScroll:
					return Input.GetAxis (NeoFpsInputManager.mouseScrollAxis);
				default:
					return NeoFpsInputManager.gamepad.GetAxis (axis);
			}
		}

		public float GetAxisRaw (FpsInputAxis axis)
		{
			if (!hasFocus || !isInputActive)
				return 0f;
			
			switch (axis)
			{
				case FpsInputAxis.MouseX:
					return Input.GetAxisRaw (NeoFpsInputManager.mouseXAxis);
				case FpsInputAxis.MouseY:
					return Input.GetAxisRaw (NeoFpsInputManager.mouseYAxis);
				case FpsInputAxis.MouseScroll:
					return Input.GetAxisRaw (NeoFpsInputManager.mouseScrollAxis);
				default:
					return NeoFpsInputManager.gamepad.GetAxisRaw (axis);
			}
		}

		public bool GetButton (FpsInputButton button)
		{
			if (!hasFocus || !isInputActive)
				return false;

			if (FpsSettings.keyBindings.GetButton(button))
				return true;
			return NeoFpsInputManager.gamepad.GetButton(button);
		}

		public bool GetButtonDown (FpsInputButton button)
		{
			if (!hasFocus || !isInputActive)
				return false;

			if (FpsSettings.keyBindings.GetButtonDown(button))
				return !NeoFpsInputManager.gamepad.GetButton(button);
			if (NeoFpsInputManager.gamepad.GetButtonDown(button))
				return !FpsSettings.keyBindings.GetButton(button);
			return false;
		}

		public bool GetButtonUp (FpsInputButton button)
		{
			if (!hasFocus || !isInputActive)
				return false;

			if (FpsSettings.keyBindings.GetButtonUp(button))
				return !NeoFpsInputManager.gamepad.GetButton(button);
			if (NeoFpsInputManager.gamepad.GetButtonUp(button))
				return !FpsSettings.keyBindings.GetButton(button);
			return false;
		}
    }
}
