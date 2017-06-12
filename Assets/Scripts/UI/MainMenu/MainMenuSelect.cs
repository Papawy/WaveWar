using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuSelect : MonoBehaviour {

	public AudioClip selectSound;
	public AudioClip backSound;
	public float Volume = 1.0f;
	public float CameraTransitionSpeed = 1.0f;

	private AudioSource source;

	private static MainMenuSelect m_instance = null;
	private static object _lock = new object();

	private bool m_mainMenuStarted = false;

	private GameObject m_activeMenu;
	private GameObject m_previousMenu;

	private bool m_joystick = false;

	public void OnMenuSelect(GameObject button)
	{
		source.PlayOneShot(selectSound, Volume);
		if(button.name == "btn_options")
		{
			JumpToMenu(GameObject.Find("options_menu"));
		}
		else if(button.name == "btn_continue")
		{
			JumpToMenu(GameObject.Find("savegames_menu"));
		}
		else if(button.name == "btn_new_game")
		{
			if(SaveManager.FindUnusedSave() != -1)
			{
				int slot = SaveManager.FindUnusedSave();
				SaveManager.Saves[slot] = new SaveGame(Application.persistentDataPath + "/Saves/SaveGame" + slot+".sav");
				SaveManager.SetActiveSave(slot);
				var sceneOp = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("city_main");
				sceneOp.allowSceneActivation = true;
			}
			else
				source.PlayOneShot(backSound, Volume);

		}
		else if(button.name == "btn_quit")
		{
			Application.Quit();
		}
		else if(button.name == "btn_changeInput" && !m_joystick)
		{
			TeamUtility.IO.InputManager.SetInputConfiguration("Joystick", TeamUtility.IO.PlayerID.One);
			m_joystick = true;
			button.GetComponent<Text>().text = "Manette : OUI";
			ControllerManager.ActiveJoystick();
		}
		else if(button.name == "btn_changeInput" && m_joystick)
		{
			TeamUtility.IO.InputManager.SetInputConfiguration("KeyboardAndMouse", TeamUtility.IO.PlayerID.One);
			m_joystick = false;
			button.GetComponent<Text>().text = "Manette : NON";
			ControllerManager.ActiveKeyboard();
		}
		else if(button.name == "btn_save_1")
		{
			if (!SaveManager.SetActiveSave(0))
			{
				SaveManager.Saves[0] = new SaveGame(Application.persistentDataPath + "/Saves/SaveGame0.sav");
				SaveManager.SetActiveSave(0);
			}
				

			var sceneOp = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("city_main");
			sceneOp.allowSceneActivation = true;
		}
		else if (button.name == "btn_save_2")
		{
			if (!SaveManager.SetActiveSave(1))
			{
				SaveManager.Saves[1] = new SaveGame(Application.persistentDataPath + "/Saves/SaveGame1.sav");
				SaveManager.SetActiveSave(1);
			}


			var sceneOp = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("city_main");
			sceneOp.allowSceneActivation = true;
		}
		else if (button.name == "btn_save_3")
		{
			if (!SaveManager.SetActiveSave(2))
			{
				SaveManager.Saves[2] = new SaveGame(Application.persistentDataPath + "/Saves/SaveGame2.sav");
				SaveManager.SetActiveSave(2);
			}
				
			var sceneOp = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("city_main");
			sceneOp.allowSceneActivation = true;
		}
		else if (button.name == "btn_save_4")
		{
			if (!SaveManager.SetActiveSave(3))
			{
				SaveManager.Saves[3] = new SaveGame(Application.persistentDataPath + "/Saves/SaveGame3.sav");
				SaveManager.SetActiveSave(3);
			}
				
			var sceneOp = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("city_main");
			sceneOp.allowSceneActivation = true;
		}
	}

	void Awake()
	{
		m_instance = this;
		//DontDestroyOnLoad(this);

		m_activeMenu = null;
		m_previousMenu = null;

		source = gameObject.GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start () {
		if (ControllerManager.JoystickActivated && ControllerManager.IsPlayerUsingController() == false)
			ControllerManager.ActiveJoystick();

		if(ControllerManager.JoystickActivated)
		{
			GameObject.Find("btn_changeInput").GetComponent<Text>().text = "Manette : OUI";
			m_joystick = true;
		}
		SaveManager.LoadSaves();
		if (SaveManager.Saves[0] != null)
		{
			GameObject.Find("btn_save_1").GetComponent<Text>().text = SaveManager.Saves[0].SaveDate.ToString();
			Debug.Log(SaveManager.Saves[0].SaveDate.ToString());
		}
		if (SaveManager.Saves[1] != null)
		{
			GameObject.Find("btn_save_2").GetComponent<Text>().text = SaveManager.Saves[1].SaveDate.ToString();
		}
		if (SaveManager.Saves[2] != null)
		{
			GameObject.Find("btn_save_3").GetComponent<Text>().text = SaveManager.Saves[2].SaveDate.ToString();
		}
		if (SaveManager.Saves[3] != null)
		{
			GameObject.Find("btn_save_4").GetComponent<Text>().text = SaveManager.Saves[3].SaveDate.ToString();
		}



	}
	
	// Update is called once per frame
	void Update () {
		if(TeamUtility.IO.InputManager.GetButton("Accept") && !m_mainMenuStarted)
		{
			foreach (MonoBehaviour script in GameObject.Find("main_menu").GetComponents<MonoBehaviour>())
			{
				script.enabled = true;
			}
			GameObject.Find("main_menu").GetComponent<Canvas>().enabled = true;
			MoveCameraTo(GameObject.Find("main_menu").transform.FindChild("CameraMarker"), 1);
			m_mainMenuStarted = true;

			m_activeMenu = GameObject.Find("main_menu");
		}
		
		if(TeamUtility.IO.InputManager.GetButton("Cancel") && m_activeMenu != null)
		{
			if(m_activeMenu != GameObject.Find("main_menu"))
			{
				source.PlayOneShot(backSound, Volume);
				JumpToMenu(m_previousMenu);
			}
		}

	}

	public void JumpToMenu(GameObject toMenu)
	{
		DeactivateMenu(m_activeMenu);
		foreach (MonoBehaviour script in toMenu.GetComponents<MonoBehaviour>())
		{
			script.enabled = true;
		}
		toMenu.GetComponent<Canvas>().enabled = true;
		MoveCameraTo(toMenu.transform.FindChild("CameraMarker"), CameraTransitionSpeed);

		m_previousMenu = m_activeMenu;
		m_activeMenu = toMenu;
	}

	public void DeactivateMenu(GameObject menu)
	{
		foreach (MonoBehaviour script in menu.GetComponents<MonoBehaviour>())
		{
			script.enabled = false;
		}
		menu.GetComponent<Canvas>().enabled = false;
	}

	public static MainMenuSelect Instance
	{
		get
		{
			lock (_lock)
			{
				if (m_instance == null)
					m_instance = new MainMenuSelect();

				return m_instance;
			}

		}
	}

	protected IEnumerator LerpToPosition(GameObject obj, float lerpSpeed, Transform starts, Transform finish, bool useRelativeSpeed = false)
	{
		if (useRelativeSpeed)
		{
			float totalDistance = starts.position.x - finish.position.x;
			float diff = obj.transform.position.x - finish.position.x;
			float multiplier = diff / totalDistance;
			lerpSpeed *= multiplier;
		}

		float t = 0.0f;
		Vector3 startingPos = obj.transform.position;
		Quaternion startingRot = obj.transform.rotation;
		while (t < 1.0f)
		{
			t += Time.deltaTime * (Time.timeScale / lerpSpeed);

			if (obj == null)
				yield return 0;
			obj.transform.position = Vector3.Lerp(startingPos, finish.position, t);
			obj.transform.rotation = Quaternion.Lerp(startingRot, finish.rotation, t);
			yield return 0;
		}
	}

	public void MoveCameraTo(Transform transform, float speed)
	{
		if (Camera.main == null)
			Debug.Log("Null main cam");

		IEnumerator coroutine = LerpToPosition(Camera.main.gameObject, speed, Camera.main.transform, transform);
		StartCoroutine(coroutine);
	}
}
