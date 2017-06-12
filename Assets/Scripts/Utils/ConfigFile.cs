using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

static class ConfigFile
{
	public static Dictionary<string, string> Values;

	private static string m_filepath;
	private static XDocument m_ConfigFile;

	public static bool Load()
	{
		try
		{
			m_ConfigFile = XDocument.Load(Application.persistentDataPath+"/config.cfg");
			m_filepath = Application.persistentDataPath + "/config.cfg";

			foreach (var valuePair in m_ConfigFile.Element("configuration").Descendants())
			{
				Values.Add(valuePair.Name.ToString(), valuePair.Value.ToString());
			}

			return true;
		}
		catch (Exception e)
		{
			m_ConfigFile = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XComment("WaveWar Configuration"),
				new XElement("configuration",
					new XElement("controller", 0),
					new XElement("last_save", 0)
				));
			m_ConfigFile.Save(m_filepath);
			return false;
		}
	}

	public static void Save()
	{
		m_ConfigFile.Save(m_filepath);
	}

	public static void SetConfig(string subPart, string configName, string value, bool createIfDontExist = false)
	{
		var userConf = m_ConfigFile.Descendants(subPart).Select(b => b.Element(configName)).FirstOrDefault();

		if (userConf != null)
			userConf.Value = value;
		else if (createIfDontExist)
		{
			m_ConfigFile.Element("configuration").Element(subPart).Add(new XElement(configName, value));
		}
	}

	public static string GetConfig(string subPart, string configName)
	{
		try
		{
			var userConf = m_ConfigFile.Descendants(subPart).Select(b => b.Element(configName)).FirstOrDefault();
			if (userConf != null)
				return userConf.Value;
			else
				return String.Empty;
		}
		catch
		{
			return String.Empty;
		}
	}
}
