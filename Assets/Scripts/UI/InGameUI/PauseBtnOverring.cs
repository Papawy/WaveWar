using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseBtnOverring : MonoBehaviour {

	public Color SelectColor = new Color(184, 42, 42);

	private Color m_originalColor;

	// Use this for initialization
	void Start () {
		m_originalColor = this.GetComponent<Text>().color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnSelected()
	{
		this.GetComponent<Text>().color = SelectColor;
	}

	public void OnDeselected()
	{
		this.GetComponent<Text>().color = m_originalColor;
	}
}
