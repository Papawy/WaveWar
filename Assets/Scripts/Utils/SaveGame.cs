using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

class SaveGame
{
	public Dictionary<string, bool> Missions;
	public int Health;
	public int SaveLocation;
	public bool Loaded;
	public DateTime SaveDate;

	private static string m_filepath;
	private static XDocument m_SaveFile;

	public SaveGame()
	{
		Loaded = true;
		Health = 100;
		SaveLocation = -1;
		Missions = new Dictionary<string, bool>();
		m_SaveFile = null;
	}

	public SaveGame(string file)
	{
		Loaded = false;
		Health = 100;
		SaveLocation = -1;
		Missions = new Dictionary<string, bool>();
		m_filepath = file;
		m_SaveFile = null;
	}

	public bool Load()
	{
		try
		{
			m_SaveFile = XDocument.Load(m_filepath);
			if (!ParseFile())
				return false;
			Loaded = true;
			return true;
		}
		catch (Exception e)
		{
			return false;
		}
	}

	private bool ParseFile()
	{
		try
		{
			if (!Int32.TryParse(m_SaveFile.Element("player_infos").Element("health").Value, out Health))
				return false;
			if (!Int32.TryParse(m_SaveFile.Element("player_infos").Element("save_location").Value, out SaveLocation))
				return false;
			if (!DateTime.TryParse(m_SaveFile.Element("player_infos").Element("date").Value, out SaveDate))
				return false;
			foreach (var mission in m_SaveFile.Element("player_infos").Element("missions").Descendants())
			{
				bool tmp = false;
				Boolean.TryParse(mission.Value, out tmp);
				Missions.Add(mission.Name.ToString(), tmp);
			}
			return true;
		}
		catch
		{
			return false;
		}
	}

	public void SaveTo(string file)
	{
		if(m_SaveFile != null)
		{
			m_SaveFile.Element("player_infos").Element("health").Value = Health.ToString();
			m_SaveFile.Element("player_infos").Element("save_location").Value = SaveLocation.ToString();
			m_SaveFile.Element("player_infos").Element("date").Value = DateTime.Now.ToString();
			m_SaveFile.Element("player_infos").Element("missions").RemoveAll();
			foreach (var mission in Missions)
			{
				m_SaveFile.Element("player_infos").Element("missions").Add(new XElement(mission.Key, mission.Value));
			}
			m_SaveFile.Save(file);
		}
		else
		{
			XElement missionsElement = new XElement("missions");
			foreach(var mission in Missions)
			{
				missionsElement.Add(new XElement(mission.Key, mission.Value));
			}
			m_SaveFile = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XComment("WaveWar SaveGame"),
				new XElement("player_infos",
					new XElement("health", Health.ToString()),
					new XElement("save_location", SaveLocation.ToString()),
					new XElement("date", DateTime.Now.ToString()),
					missionsElement
				));
			m_SaveFile.Save(file);
		}
		Debug.Log(file);
	}

	public void Save()
	{
		if (m_SaveFile != null)
			m_SaveFile.Save(m_filepath);
		else
			SaveTo(m_filepath);
	}
}
