using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalScript : MonoBehaviour {

	public static System.Random Random = new System.Random();

	public static bool DebugMode = false;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static bool IsPlayerUsingController()
	{
		return TeamUtility.IO.InputManager.PlayerOneConfiguration.name == "Joystick";
	}
}
