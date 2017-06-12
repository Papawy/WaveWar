using System;

static class ControllerManager
{
	public static bool JoystickActivated = false;

	public static void ActiveJoystick()
	{
		TeamUtility.IO.InputManager.SetInputConfiguration("Joystick", TeamUtility.IO.PlayerID.One);
		JoystickActivated = true;
	}

	public static void ActiveKeyboard()
	{
		TeamUtility.IO.InputManager.SetInputConfiguration("KeyboardAndMouse", TeamUtility.IO.PlayerID.One);
		JoystickActivated = false;
	}

	public static bool IsPlayerUsingController()
	{
		return TeamUtility.IO.InputManager.PlayerOneConfiguration.name == "Joystick";
	}
}
