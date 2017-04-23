using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker2D : MonoBehaviour {

	public float YVariation = 0.1f;

	public GameObject MarkerText = null;

	float m_originalPosY;
	bool m_up = true;

	bool m_variation = true;

	// Use this for initialization
	void Start () {
		m_originalPosY = this.transform.localPosition.y;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_variation)
		{
			if (m_up)
			{
				if (this.transform.localPosition.y > m_originalPosY + YVariation)
					m_up = false;
				else
					this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + YVariation * Time.deltaTime, this.transform.localPosition.z);
			}
			else
			{
				if (this.transform.localPosition.y < m_originalPosY)
					m_up = true;
				else
					this.transform.localPosition = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y - YVariation * Time.deltaTime, this.transform.localPosition.z);
			}
		}
	}

	public void ToggleYVariation(bool toggle)
	{
		m_variation = toggle;
	}

	public void ToggleMarker(bool toggle)
	{
		if(MarkerText != null && MarkerText.GetComponent<UnityEngine.UI.Text>() != null)
		{
			if (toggle)
				MarkerText.GetComponent<UnityEngine.UI.Text>().text = "V";
			else
				MarkerText.GetComponent<UnityEngine.UI.Text>().text = "";
		}
	}

	public void SetMarkerColor(Color color)
	{
		if (MarkerText != null && MarkerText.GetComponent<UnityEngine.UI.Text>() != null)
		{
			MarkerText.GetComponent<UnityEngine.UI.Text>().color = color;
		}
	}
}
