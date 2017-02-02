using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelect : MonoBehaviour {

	protected List<GameObject> textArray = null;


	private int m_currMenSel = 0;

	// Use this for initialization
	void Start () {

		textArray = new List<GameObject>();

		foreach(Transform child in transform)
		{
			if(child.gameObject.tag == "button")
			{
				textArray.Add(child.gameObject);
			}
				
		}

		if (textArray.Count == 0)
			this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (InputManager.Instance.GetKeyDown("back"))
		{
			if (m_currMenSel < textArray.Count - 1)
			{
				m_currMenSel++;
				textArray[m_currMenSel - 1].GetComponent<BtnOverring>().OnDeselected();
			}
			else
			{
				m_currMenSel = 0;
				textArray[textArray.Count - 1].GetComponent<BtnOverring>().OnDeselected();
			}
		}
		else if (InputManager.Instance.GetKeyDown("forward"))
		{
			if (m_currMenSel > 0)
			{
				m_currMenSel--;
				textArray[m_currMenSel + 1].GetComponent<BtnOverring>().OnDeselected();
			}
			else
			{
				m_currMenSel = textArray.Count - 1;
				textArray[0].GetComponent<BtnOverring>().OnDeselected();
			}
		}
		else if (InputManager.Instance.GetKeyDown("accept"))
		{
			BroadcastMessage("OnMenuSelect", textArray[m_currMenSel], SendMessageOptions.DontRequireReceiver);
			MainMenuSelect.Instance.OnMenuSelect(textArray[m_currMenSel]);
		}
		textArray[m_currMenSel].GetComponent<BtnOverring>().OnSelected();
	}
}
