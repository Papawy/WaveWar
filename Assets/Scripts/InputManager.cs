using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	private Dictionary<string, KeyCode> m_keyDict;


	private static InputManager m_instance = null;
	private static object _lock = new object();

	public void Awake()
	{
		m_instance = this;
		DontDestroyOnLoad(this);

		m_keyDict.Add("forward", (KeyCode)PlayerPrefs.GetInt("forward", (int)KeyCode.Z));
		m_keyDict.Add("back", (KeyCode)PlayerPrefs.GetInt("back", (int)KeyCode.S));
		m_keyDict.Add("left", (KeyCode)PlayerPrefs.GetInt("left", (int)KeyCode.Q));
		m_keyDict.Add("right", (KeyCode)PlayerPrefs.GetInt("right", (int)KeyCode.D));
		m_keyDict.Add("jump", (KeyCode)PlayerPrefs.GetInt("jump", (int)KeyCode.Space));
		m_keyDict.Add("accept", (KeyCode)PlayerPrefs.GetInt("jump", (int)KeyCode.Return));
	}

	public static InputManager Instance
	{
		get
		{
			lock (_lock)
			{
				if (m_instance == null)
					m_instance = new InputManager();

				return m_instance;
			}

		}
	}

	private InputManager()
	{
		m_keyDict = new Dictionary<string, KeyCode>();
	}

	public void SaveKeyPrefs()
	{
		foreach (KeyValuePair<string, KeyCode> key in m_keyDict)
		{
			PlayerPrefs.SetInt(key.Key, (int)key.Value);
		}
		PlayerPrefs.Save();
	}

	public void SetKey(string name, KeyCode key)
	{
		m_keyDict[name] = key;
	}

	public bool GetKeyDown(string name)
	{
		if (m_keyDict.ContainsKey(name))
			return Input.GetKeyDown(m_keyDict[name]);
		else
			return false;
	}

	public Dictionary<string, KeyCode> GetKeyDict()
	{
		return m_keyDict;
	}
}
