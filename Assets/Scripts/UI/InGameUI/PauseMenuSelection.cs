using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuSelection : MonoBehaviour {

	public AudioClip selectSound;
	public AudioClip backSound;
	public float Volume = 1.0f;
	public float CameraTransitionSpeed = 1.0f;

	private AudioSource source;

	private bool m_joystick = false;

	protected List<GameObject> textArray = null;

	private int m_currMenSel = 0;

	private float m_timer = 0.0f;

	public void OnMenuSelect(GameObject button)
	{
		source.PlayOneShot(selectSound, Volume);
		if (button.name == "btn_options")
		{
			
		}
		else if (button.name == "btn_resume")
		{
			/*Time.timeScale = 1;
			this.gameObject.SetActive(true);*/
			GameObject.Find("HeroeTest").GetComponent<PlayerController>().ResumeFromPause();
		}
		else if (button.name == "btn_quit")
		{
			GameObject.Find("HeroeTest").GetComponent<PlayerController>().ResumeFromPause();
			var sceneOp = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("mainmenu");
			sceneOp.allowSceneActivation = true;
		}
	}

	void Awake()
	{

		source = Camera.main.gameObject.GetComponent<AudioSource>();
	}

	// Use this for initialization
	void Start()
	{
		textArray = new List<GameObject>();

		foreach (Transform child in transform)
		{
			if (child.gameObject.tag == "button")
			{
				textArray.Add(child.gameObject);
			}

		}

		if (textArray.Count == 0)
			this.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{

		if (TeamUtility.IO.InputManager.GetButton("Cancel"))
		{
			source.PlayOneShot(backSound, Volume);
		}

		if (TeamUtility.IO.InputManager.GetAxisRaw("MenuVertical") <= -0.9 && Time.unscaledTime - m_timer > 0.20f)
		{
			if (m_currMenSel < textArray.Count - 1)
			{
				m_currMenSel++;
				textArray[m_currMenSel - 1].GetComponent<PauseBtnOverring>().OnDeselected();
			}
			else
			{
				m_currMenSel = 0;
				textArray[textArray.Count - 1].GetComponent<PauseBtnOverring>().OnDeselected();
			}
			m_timer = Time.unscaledTime;
		}
		else if (TeamUtility.IO.InputManager.GetAxisRaw("MenuVertical") >= 0.9 && Time.unscaledTime - m_timer > 0.20f)
		{
			
			if (m_currMenSel > 0)
			{
				m_currMenSel--;
				textArray[m_currMenSel + 1].GetComponent<PauseBtnOverring>().OnDeselected();
			}
			else
			{
				m_currMenSel = textArray.Count - 1;
				textArray[0].GetComponent<PauseBtnOverring>().OnDeselected();
			}
			m_timer = Time.unscaledTime;
		}
		else if (TeamUtility.IO.InputManager.GetButtonDown("Accept"))
		{

			OnMenuSelect(textArray[m_currMenSel]);
		}
		textArray[m_currMenSel].GetComponent<PauseBtnOverring>().OnSelected();
	}
}
