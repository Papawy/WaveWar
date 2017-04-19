using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsBox : MonoBehaviour {

	public UnityEngine.UI.Text TextTipsBox = null;

	protected float time = 0;
	protected uint timeInterval = 3000;

	// Use this for initialization
	void Start () {
		if(TextTipsBox != null)
		{
			TextTipsBox.gameObject.SetActive(false);
			ShowTips("ZQSD pour vous déplacer\r\nClique droit pour viser\r\nClique gauche pour tirer\r\nE pour intéragir avec l'environnement", 5000);
		}
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime*1000;
		if(time > timeInterval)
		{
			TextTipsBox.text = "";
			TextTipsBox.gameObject.SetActive(false);
		}
	}

	public void ShowTips(string text, uint timeInMs = 3000)
	{
		if(TextTipsBox != null)
		{
			TextTipsBox.text = text;
			TextTipsBox.gameObject.SetActive(true);
			timeInterval = timeInMs;
			time = 0;
		}
	}
}
