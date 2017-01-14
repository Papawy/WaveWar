using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelect : MonoBehaviour {

	public UnityEngine.UI.Text[] textArray = null;
	public float TimeInterval = 0.5f;


	public int m_currMenSel = 0;

	private float m_selTime = 0.0f;

	// Use this for initialization
	void Start () {
		if (textArray.Length == 0)
			this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - m_selTime > TimeInterval)
		{
			if (Input.GetAxis("Vertical") < -0.1)
			{
				if (m_currMenSel < textArray.Length - 1)
				{
					m_currMenSel++;
					textArray[m_currMenSel - 1].GetComponent<BtnOverring>().OnDeselected();
				}
				else
				{
					m_currMenSel = 0;
					textArray[textArray.Length - 1].GetComponent<BtnOverring>().OnDeselected();
				}
			}
			else if (Input.GetAxis("Vertical") > 0.1)
			{
				if (m_currMenSel > 0)
				{
					m_currMenSel--;
					textArray[m_currMenSel + 1].GetComponent<BtnOverring>().OnDeselected();
				}
				else
				{
					m_currMenSel = textArray.Length-1;
					textArray[0].GetComponent<BtnOverring>().OnDeselected();
				}
			}
			textArray[m_currMenSel].GetComponent<BtnOverring>().OnSelected();
			m_selTime = Time.time;
		}


	}
}
