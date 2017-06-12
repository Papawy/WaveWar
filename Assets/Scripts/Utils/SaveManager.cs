using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

static class SaveManager
{
	public static SaveGame[] Saves;
	public static SaveGame ActiveSave = null;

	public static int LoadSaves()
	{
		Saves = new SaveGame[4];
		int loadedSaves = 0;

		if(!System.IO.Directory.Exists(Application.persistentDataPath + "/Saves"))
			System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/Saves");

		for(int i = 0; i< Saves.Length; i++)
		{
			try
			{
				SaveGame tmp = new SaveGame(Application.persistentDataPath+"/Saves/SaveGame" + i + ".sav");
				if (tmp.Load())
				{
					loadedSaves += 1;
					Saves[i] = tmp;
				}
				else
					Saves[i] = null;
			}
			catch
			{
				Saves[i] = null;
			}
		}
		return loadedSaves;
	}

	public static bool SetActiveSave(int slot)
	{
		if (slot == -1)
		{
			ActiveSave = null;
			return true;
		}

		if (slot < 0 || slot >= 4)
			throw new ArgumentException("Wrong slot");

		if (Saves[slot] != null)
		{
			ActiveSave = Saves[slot];
			return true;
		}
		else
			return false;
	}

	public static int FindUnusedSave()
	{
		int tmp = -1;

		for (int i = 0; i < Saves.Length; i++)
		{
			if (Saves[i] == null)
				tmp = i;
		}

		return tmp;
	}
}
