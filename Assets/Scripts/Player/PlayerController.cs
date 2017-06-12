using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

	public float Speed = 4.0f;

	Animator anim;

	GameObject m_preInterObjHit = null;

	public GameObject AttackLabel = null;

	public GameObject Guitar = null;

	public GameObject PauseMenu = null;
	public GameObject HUD = null;

	public bool IsPaused = false;

	private uint attackLaunchTime = 0;
	private uint comboTime = 0;
	private bool comboLaunched = false;
	private bool comboInPreparation = false;
	private float comboErrorMargin = 0.2f;
	private int comboChain = 0;

	private float comboMult = 1;

	public uint MaxComboTime = 2000;
	public uint MinComboTime = 200;

	public void ResumeFromPause()
	{
		PauseMenu.SetActive(false);
		HUD.SetActive(true);
		Time.timeScale = 1;
		IsPaused = false;
		Camera.main.GetComponent<OrbitCamera>().enabled = true;
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();

		SetSaveLocation();
	}
	
	public void ToggleHUD(bool toggle)
	{
		if(toggle)
		{
			if(IsPaused)
			{
				PauseMenu.SetActive(true);
			}
			else
			{
				HUD.SetActive(true);
			}
		}
		else
		{
			HUD.SetActive(false);
			PauseMenu.SetActive(false);
		}
	}

	public void PausePlayer(bool pause)
	{
		if(pause)
		{
			PauseMenu.SetActive(true);
			HUD.SetActive(false);
			Time.timeScale = 0;
			IsPaused = true;
			Camera.main.GetComponent<OrbitCamera>().enabled = false;
		}
		else
		{
			PauseMenu.SetActive(false);
			HUD.SetActive(true);
			Time.timeScale = 1;
			IsPaused = false;
			Camera.main.GetComponent<OrbitCamera>().enabled = true;
		}
	}

	// Update is called once per frame
	void Update () {

		if (TeamUtility.IO.InputManager.GetButtonDown("DebugMode"))
		{
			GlobalScript.DebugMode = true;
		}

		if(TeamUtility.IO.InputManager.GetButtonDown("Pause"))
		{
			if (PauseMenu.activeSelf)
			{
				PauseMenu.SetActive(false);
				HUD.SetActive(true);
				Time.timeScale = 1;
				IsPaused = false;
				Camera.main.GetComponent<OrbitCamera>().enabled = true;
			}
			else
			{
				PauseMenu.SetActive(true);
				HUD.SetActive(false);
				Time.timeScale = 0;
				IsPaused = true;
				Camera.main.GetComponent<OrbitCamera>().enabled = false;
			}
				
		}
		/*
		if (TeamUtility.IO.InputManager.GetButtonDown("Accept"))
		{
			GameObject.Find("NPC_1").GetComponent<CharacterController>().MoveTo(this.transform.position);
		}
		if (TeamUtility.IO.InputManager.GetButtonDown("Cancel"))
		{
			GameObject.Find("NPC_1").GetComponent<CharacterController>().MoveToNode();
		}*/
		if(!IsPaused)
		{

			if (m_preInterObjHit != null)
			{
				m_preInterObjHit.GetComponentInChildren<Marker2D>().ToggleMarker(false);
				m_preInterObjHit = null;
			}

			RaycastHit rayHit;
			if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out rayHit, 3.0f))
			{
				if (rayHit.transform.gameObject.GetComponent<InteractionManager>() != null)
				{
					m_preInterObjHit = rayHit.transform.gameObject;
					rayHit.transform.gameObject.GetComponentInChildren<Marker2D>().ToggleMarker(true);
				}
				else
					m_preInterObjHit = null;
			}

			float move = (Mathf.Abs(TeamUtility.IO.InputManager.GetAxisRaw("Vertical")) + Mathf.Abs(TeamUtility.IO.InputManager.GetAxisRaw("Horizontal")));
			move = Mathf.Clamp(move, 0.0f, 0.5f);
			Quaternion camRot = Quaternion.Euler(0.0f, Camera.main.transform.rotation.eulerAngles.y + Mathf.Atan(TeamUtility.IO.InputManager.GetAxisRaw("Horizontal") / (TeamUtility.IO.InputManager.GetAxisRaw("Vertical") + 0.0001f)) * (180 / Mathf.PI), 0);

			if (TeamUtility.IO.InputManager.GetAxisRaw("Vertical") < 0)
			{
				this.transform.rotation = Quaternion.Euler(0.0f, camRot.eulerAngles.y + (180 * Mathf.Sign(TeamUtility.IO.InputManager.GetAxisRaw("Horizontal") * -1)), 0);
				//this.transform.position += transform.forward * Speed * move * Time.deltaTime;
			}
			else if (move != 0.0f)
			{
				this.transform.rotation = camRot;
				//this.transform.position += transform.forward * Speed * move * Time.deltaTime;
			}

			if (m_preInterObjHit != null && TeamUtility.IO.InputManager.GetButtonDown("Interact"))
			{
				m_preInterObjHit.GetComponent<InteractionManager>().Interact();
			}
			if (TeamUtility.IO.InputManager.GetButton("Interact") && GlobalScript.DebugMode)
			{
				this.gameObject.GetComponent<PlayerStats>().Life -= 0.5f;
			}

			/*if (TeamUtility.IO.InputManager.GetButtonDown("NextWeapon"))
			{
				switch (this.gameObject.GetComponent<AttackManager>().CurrentAttackType)
				{
					case AttackManager.TYPE.NORMAL:
						this.gameObject.GetComponent<AttackManager>().CurrentAttackType = AttackManager.TYPE.PRECISION;
						AttackLabel.GetComponent<UnityEngine.UI.Text>().text = "Si (précision)";
						break;

					case AttackManager.TYPE.PRECISION:
						this.gameObject.GetComponent<AttackManager>().CurrentAttackType = AttackManager.TYPE.LARGE;
						AttackLabel.GetComponent<UnityEngine.UI.Text>().text = "Do (large)";
						break;

					case AttackManager.TYPE.LARGE:
						this.gameObject.GetComponent<AttackManager>().CurrentAttackType = AttackManager.TYPE.NORMAL;
						AttackLabel.GetComponent<UnityEngine.UI.Text>().text = "Sol (normal)";
						break;
				}
			}

			if (TeamUtility.IO.InputManager.GetButtonDown("PrevWeapon"))
			{
				switch (this.gameObject.GetComponent<AttackManager>().CurrentAttackType)
				{
					case AttackManager.TYPE.NORMAL:
						this.gameObject.GetComponent<AttackManager>().CurrentAttackType = AttackManager.TYPE.LARGE;
						AttackLabel.GetComponent<UnityEngine.UI.Text>().text = "Do (large)";
						break;

					case AttackManager.TYPE.PRECISION:
						this.gameObject.GetComponent<AttackManager>().CurrentAttackType = AttackManager.TYPE.NORMAL;
						AttackLabel.GetComponent<UnityEngine.UI.Text>().text = "Sol (normal)";
						break;

					case AttackManager.TYPE.LARGE:
						this.gameObject.GetComponent<AttackManager>().CurrentAttackType = AttackManager.TYPE.PRECISION;
						AttackLabel.GetComponent<UnityEngine.UI.Text>().text = "Si (précision)";
						break;
				}
			}*/

			if (TeamUtility.IO.InputAdapter.GetButton("Aim"))
			{
				this.transform.rotation = camRot;
				anim.SetBool("Guitar", true);
				Guitar.transform.parent = this.gameObject.transform.FindChild("Gloves");
				Guitar.transform.localPosition = new Vector3(-0.04f, 1.009f, 0.161f);
				Guitar.transform.localEulerAngles = new Vector3(-2.383f, -95.204f, 90.63f);
				if (TeamUtility.IO.InputManager.GetButtonDown("Attack"))
				{
					bool noteChosen = false;
					UnityEngine.UI.Text comboText = HUD.transform.FindChild("ComboText").GetComponent<UnityEngine.UI.Text>();
					if (TeamUtility.IO.InputManager.GetButton("AttackNoteLarge"))
					{
						this.gameObject.GetComponent<AttackManager>().CurrentAttackType = AttackManager.TYPE.LARGE;
						noteChosen = true;
					}
					else if(TeamUtility.IO.InputManager.GetButton("AttackNoteNormal"))
					{
						this.gameObject.GetComponent<AttackManager>().CurrentAttackType = AttackManager.TYPE.NORMAL;
						noteChosen = true;
					}
					else if(TeamUtility.IO.InputManager.GetButton("AttackNotePrecision"))
					{
						this.gameObject.GetComponent<AttackManager>().CurrentAttackType = AttackManager.TYPE.PRECISION;
						noteChosen = true;
					}
					if(noteChosen)
					{
						if (comboLaunched == false)
						{
							comboInPreparation = true;
							comboLaunched = true;
							attackLaunchTime = (uint)(Time.time * 1000);
							comboChain = 0;
							this.gameObject.GetComponent<AttackManager>().LaunchAttack(comboMult);
						}
						else
						{
							if (comboInPreparation == true)
							{
								comboTime = (uint)(Time.time * 1000) - attackLaunchTime;
								attackLaunchTime = (uint)(Time.time * 1000);
								comboInPreparation = false;
								this.gameObject.GetComponent<AttackManager>().LaunchAttack(comboMult);
								comboChain = 1;
							}
							else
							{
								//Debug.Log("Margin : " + (comboTime - comboTime * comboErrorMargin) + " to " + (comboTime + comboTime * comboErrorMargin) + " Actual time : " + ((uint)(Time.time * 1000) - attackLaunchTime));
								if ((uint)(Time.time * 1000) - attackLaunchTime < comboTime - comboTime * comboErrorMargin || (uint)(Time.time * 1000) - attackLaunchTime > comboTime + comboTime * comboErrorMargin)
								{
									comboLaunched = false;
									comboText.text = "Bad Tempo !";
									comboMult = 1;
									this.gameObject.GetComponent<AttackManager>().LaunchAttack(comboMult);
									ResetComboText(3000);
								}
								else
								{
									comboChain += 1;
									comboMult += 0.1f * Mathf.Log(comboChain);
									comboText.enabled = true;
									comboText.text = "Combo : x" + comboMult.ToString("0.");
									this.gameObject.GetComponent<AttackManager>().LaunchAttack(comboMult);
									attackLaunchTime = (uint)(Time.time * 1000);
								}
							}
						}
					}
					
				}
			}
			else
			{
				anim.SetBool("Guitar", false);
				Guitar.transform.parent = this.gameObject.transform.FindChild("mixamorig:Hips/mixamorig:Spine");
				Guitar.transform.localPosition = new Vector3(0.08499992f, 0.394f, -0.1890006f);
				Guitar.transform.localEulerAngles = new Vector3(-63.272f, 44.311f, 131.031f);
			}

			if (comboLaunched == true && (uint)(Time.time * 1000) - attackLaunchTime > MaxComboTime)
			{
				UnityEngine.UI.Text comboText = HUD.transform.FindChild("ComboText").GetComponent<UnityEngine.UI.Text>();
				comboLaunched = false;
				comboInPreparation = false;
				comboText.text = "Bad Tempo !";
				comboMult = 1;
			}

			if (TeamUtility.IO.InputAdapter.GetButton("Sprint"))
				move = move * 2;

			anim.SetFloat("Speed", move);
		}

	}

	protected void SetSaveLocation()
	{
		StartCoroutine(SetSaveLocationCoroutine());
	}

	protected IEnumerator SetSaveLocationCoroutine()
	{
		yield return new WaitForSeconds(0.05f);
		if(SaveManager.ActiveSave.SaveLocation == 0)
		{
			Vector3 pos = GameObject.Find("HouseCityTP").transform.position;
			this.gameObject.transform.position = new Vector3(pos.x - 3, pos.y-1, pos.z);
		}
		else if(SaveManager.ActiveSave.SaveLocation == -1)
		{
			Vector3 pos = GameObject.Find("GameEarlyStart").transform.position;
			this.gameObject.transform.position = new Vector3(pos.x, pos.y - 1, pos.z);
		}
	}

	protected void ResetComboText(int time)
	{
		StartCoroutine(ResetComboTextCallback(time));
	}

	private IEnumerator ResetComboTextCallback(int time)
	{
		yield return new WaitForSeconds((float)(time / 1000));
		HUD.transform.FindChild("ComboText").GetComponent<UnityEngine.UI.Text>().enabled = false;
	}
}
