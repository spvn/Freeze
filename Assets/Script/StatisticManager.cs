using System.Linq;
using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class Statistic {
	public string name;
	public float progress = 0.0f;
	public Achievement[] achievements;
	
	public void addProgress (float progress)
	{
		this.progress += progress;
		Debug.Log ("Statistic \"" + this.name + "\" has been updated! (" + progress + ")");
		this.updateAchievements ();
	}
	
	public void setProgress (float progress) 
	{
		this.progress = progress;
		Debug.Log ("Statistic \"" + this.name + "\" has been updated! (" + progress + ")");
		this.updateAchievements ();
	}
	
	private void updateAchievements ()
	{
		foreach (Achievement ac in achievements) {
			ac.setProgress(this.progress);
		}
	}

	public float getProgress()
	{
		return this.progress;
	}
}

[System.Serializable]
public class StatisticManager : MonoBehaviour {
	private	static StatisticManager _instance;
	public Statistic[] stats;
	
	void Awake () {
		if (_instance == null) {
			_instance = this;
			DontDestroyOnLoad (this);
			this.loadStatistics();
		} else {
			if (this != _instance) {
				Destroy(this.gameObject);
			}
		}
	}
	
	public static StatisticManager getManager()
	{
		return _instance;
	}
	
	public Statistic getStatisticByName(string statisticName)
	{
		return stats.FirstOrDefault(statistic => statistic.name == statisticName);
	}
	
	public void addProgressByStatisticName(string statisticName, float progressAmount)
	{
		Statistic stat = this.getStatisticByName (statisticName);
		stat.addProgress (progressAmount);
		this.saveStatistics ();
	}
	
	public void setProgressByStatisticName(string statisticName, float progressAmount)
	{
		Statistic stat = this.getStatisticByName (statisticName);
		stat.setProgress (progressAmount);
		this.saveStatistics ();
	}

	public float getProgressByStatisticName(string statisticName)
	{
		Statistic stat = this.getStatisticByName (statisticName);
		return stat.getProgress ();
	}

	public void saveStatistics ()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savefile.gd");
		bf.Serialize(file, stats);
		file.Close ();
		Debug.Log("Saved stats to: " + Application.persistentDataPath + "/savefile.gd");
	}

	public void loadStatistics()
	{
		if (File.Exists (Application.persistentDataPath + "/savefile.gd")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/savefile.gd", FileMode.Open);
			stats = (Statistic[])bf.Deserialize (file);
			file.Close ();
			Debug.Log ("Loaded stats from: " + Application.persistentDataPath + "/savefile.gd");
		} else {
			Debug.Log ("Old stats not found. Creating new file.");
			this.saveStatistics();
		}
	}
}
